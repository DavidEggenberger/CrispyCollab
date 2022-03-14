using AuthPermissions;
using AuthPermissions.AspNetCore;
using AuthPermissions.AspNetCore.Services;
using AuthPermissions.SetupCode;
using Common;
using Domain.Interfaces;
using FluentValidation.AspNetCore;
using Identity.Interfaces;
using Infrastructure.CQRS;
using Infrastructure.EmailSender;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Types.Overrides;
using Infrastructure.Persistence;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using WebServer.Hubs;
using WebServer.SignalR;

namespace WebServer
{
    public class Startup
    {
        public IWebHostEnvironment webHostEnvironment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Framework
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Identity/TeamManagement", "TeamAdmin");
                options.Conventions.AuthorizeFolder("/Identity");
                options.Conventions.AllowAnonymousToFolder("/Identity/Stripe");
                options.Conventions.AllowAnonymousToPage("/Identity/Login");
                options.Conventions.AllowAnonymousToPage("/Identity/SignUp");
                options.Conventions.AllowAnonymousToPage("/Identity/TwoFactorLogin");
                options.Conventions.AllowAnonymousToPage("/Identity/LoginWithRecoveryCode");
            });
            services.AddControllers(options =>
            {
                //AuthorizeFilterBehaviour
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddFluentValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
                options.RegisterValidatorsFromAssembly(typeof(Common.IAssemblyMarker).Assembly);
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Type = $"https://httpstatuses.com/400",
                        Detail = "ApiConstants.Messages.ModelStateValidation"
                    };
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes =
                        {
                            "ApiConstants.ContentTypes.ProblemJson",
                            "ApiConstants.ContentTypes.ProblemXml"
                        }
                    };
                };
            });
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.Name = "__Host-X-XSRF-TOKEN";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            #endregion
            services.AddAutoMapper(GetType().Assembly);
            services.AddCQRS(typeof(Application.IAssemblyMarker).Assembly);
            StripeConfiguration.ApiKey = Configuration["StripeKey"];
            services.Configure<SendGridEmailOptions>(Configuration);
            services.AddTransient<IEmailSender, SendGridEmailSender>();
            services.AddScoped<IAggregatesUINotifierService, AggregatesUINotifierService>();
            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration["AzureSQLConnection"]);
            //});
            #region Identity
            services.AddScoped<ApplicationUserTeamManager>();
            services.AddScoped<IIdentityUINotifierService, IdentityUINotifierService>();
            services.AddScoped<SubscriptionManager>();
            services.AddScoped<SubscriptionPlanManager>();
            services.AddScoped<TeamManager>();
            services.AddScoped<AdminNotificationManager>();
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy("TeamUser", options =>
                {
                    options.RequireClaim(ClaimConstants.TeamIdClaimType);
                    options.RequireClaim(ClaimConstants.TeamRoleClaimType, "User", "Admin");
                });
                options.AddPolicy("TeamAdmin", options =>
                {
                    options.RequireClaim(ClaimConstants.TeamIdClaimType);
                    options.RequireClaim(ClaimConstants.TeamRoleClaimType, "Admin");
                });
            });

            services.AddDbContext<IdentificationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDbLocalConnectionString"));
            });

            AuthenticationBuilder authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddLinkedIn(options =>
                {
                    options.ClientId = "test";
                    options.ClientSecret = "test";
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = "test";
                    options.ClientSecret = "test";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["SocialLogins:Google:ClientId"];
                    options.ClientSecret = Configuration["SocialLogins:Google:ClientSecret"];
                    options.Scope.Add("profile");
                    options.Events.OnCreatingTicket = (context) =>
                    {
                        var picture = context.User.GetProperty("picture").GetString();
                        context.Identity.AddClaim(new Claim("picture", picture));
                        return Task.CompletedTask;
                    };
                });
            authenticationBuilder.AddExternalCookie().Configure(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "ExternalAuthenticationCookie";
            });
            authenticationBuilder.AddApplicationCookie().Configure(options =>
            {
                options.ExpireTimeSpan = new TimeSpan(6, 0, 0);
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = "AuthenticationCookie";
                options.LoginPath = "/Identity/Login";
                options.LogoutPath = "/Identity/User/Logout";
                options.SlidingExpiration = true;
            });
            authenticationBuilder.AddTwoFactorUserIdCookie().Configure(options =>
            {
                options.Cookie.Name = "TwoFAUserIdCookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            authenticationBuilder.AddTwoFactorRememberMeCookie().Configure(options =>
            {
                options.Cookie.Name = "TwoFARememberMeCookie";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            var identityService = services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = true;
                options.Stores.MaxLengthForKeys = 128;
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.Sid;
                options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
            })
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory<ApplicationUser>>()
                .AddUserManager<ApplicationUserManager>()
                .AddEntityFrameworkStores<IdentificationDbContext>()
                .AddSignInManager();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseExceptionHandler("/exceptionHandler");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>(EndpointConstants.NotificationHub);
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
