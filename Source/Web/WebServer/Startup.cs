using FluentValidation.AspNetCore;
using Infrastructure.CQRS;
using Infrastructure.EmailSender;
using Infrastructure.Identity;
using Infrastructure.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Stripe;
using WebShared.Authorization;
using Infrastructure.MultiTenancy;
using Infrastructure.SignalR;
using System.Reflection;
using WebServer.Modules.ExceptionHandling;
using WebServer.Modules.ModelValidation;

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
            })
                .AddFluentValidation(options =>
                {
                    options.DisableDataAnnotationsValidation = true;
                    options.RegisterValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);
                })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    });

            services.RegisterModelValidation();
            services.RegisterAutoMapper();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.Name = "__Host-X-XSRF-TOKEN";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            
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
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseExceptionHandling();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMultiTenancyMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSignalR();
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
