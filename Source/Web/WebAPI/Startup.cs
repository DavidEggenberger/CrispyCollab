using AuthPermissions;
using AuthPermissions.AspNetCore;
using AuthPermissions.AspNetCore.Services;
using AuthPermissions.SetupCode;
using Common.EnvironmentService;
using Infrastructure.CQRS;
using Infrastructure.EmailSender;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Types.Overrides;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI
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
            StripeConfiguration.ApiKey = Configuration["StripeKey"];
            #if DEBUG
            TelemetryConfiguration.Active.DisableTelemetry = true;
            TelemetryDebugWriter.IsTracingDisabled = true;
            #endif
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddTransient<IEmailSender, SendGridEmailSender>();
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Identity");
                options.Conventions.AllowAnonymousToPage("/Login");
                options.Conventions.AllowAnonymousToPage("/SignUp");
                options.Conventions.AllowAnonymousToPage("/TwoFactorLogin");
            });
            services.AddControllers(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddScoped<IEnvironmentService, ServerEnvironmentService>();
            services.AddScoped<TenantManager>();
            services.AddScoped<ApplicationUserTenantManager>();
            services.AddCQRS(GetType().Assembly);

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.Name = "__Host-X-XSRF-TOKEN";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration["AzureSQLConnection"]);
            //});

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy("TenantGuest", options =>
                {
                    options.RequireClaim("TenantId");
                    options.RequireClaim("TenantRole", "Guest", "User", "Admin");
                });
                options.AddPolicy("TenantUser", options =>
                {
                    options.RequireClaim("TenantId");
                    options.RequireClaim("TenantRole", "User", "Admin");
                });
                options.AddPolicy("TenantAdmin", options =>
                {
                    options.RequireClaim("TenantId");
                    options.RequireClaim("TenantRole", "Admin");
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
                options.ExpireTimeSpan = new TimeSpan(0, 60, 0);
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
                options.ClaimsIdentity.UserNameClaimType = ClaimTypes.NameIdentifier;
            })
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory<ApplicationUser>>()
                .AddUserManager<ApplicationUserManager>()
                .AddEntityFrameworkStores<IdentificationDbContext>()
                .AddSignInManager();
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
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
