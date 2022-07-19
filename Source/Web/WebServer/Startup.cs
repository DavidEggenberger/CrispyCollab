using WebShared;
using FluentValidation.AspNetCore;
using Identity.Interfaces;
using Infrastructure.CQRS;
using Infrastructure.EmailSender;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Infrastructure.Interfaces;
using Infrastructure.EFCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebServer.Hubs;
using WebServer.Services;
using WebServer.SignalR;
using WebServer.Authorization;
using Infrastructure.Stripe;
using WebShared.Authorization;
using Infrastructure.MultiTenancy;

namespace WebServer
{
    public class Startup
    {
        public IWebHostEnvironment WebHostEnvironment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
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
                options.RegisterValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);
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
            services.AddAutoMapper(typeof(IAssemblyMarker).Assembly);
            #endregion
            #region Infrastructure
            services.RegisterCQRS();
            services.RegisterEmailSender(Configuration);
            services.RegisterEFCore(Configuration);
            services.RegisterStripe(Configuration);
            services.RegisterIdentity(Configuration);
            services.RegisterSignalR();
            services.RegisterAuthorization();
            services.RegisterMultiTenancy();    
            services.RegisterTeamManagement();
            services.AddScoped<IAuthorizationHandler, CreatorPolicyHandler>();
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
