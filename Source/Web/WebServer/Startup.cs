using FluentValidation.AspNetCore;
using Infrastructure.CQRS;
using Infrastructure.EmailSender;
using Infrastructure.Identity;
using Infrastructure.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebShared.Authorization;
using Infrastructure.MultiTenancy;
using WebServer.Modules.ModelValidation;
using WebServer.Modules.HostingInformation;
using Infrastructure.RedisCache;
using WebServer.Modules.Swagger;
using Infrastructure.StripeIntegration;
using WebServer.SignalR;

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
                options.Conventions.AllowAnonymousToFolder("/LandingPages");
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
            #endregion

            #region Modules
            services.RegisterModelValidation();
            services.RegisterAutoMapper();
            services.RegisterServerInformationProvider();
            services.RegisterSwagger();
            services.RegisterApiVersioning();
            #endregion

            #region Infrastructure
            services.RegisterCQRS();
            services.RegisterEmailSender(Configuration);
            services.RegisterEFCore(Configuration);
            services.RegisterMultiTenancy();
            services.RegisterRedisCache(Configuration);
            services.RegisterStripe(Configuration);
            services.RegisterIdentity(Configuration);
            services.RegisterSignalR();
            #endregion

            services.RegisterAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSecurityHeaders();
            app.UseExceptionHandling();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerMiddleware();
            app.UseApiVersioningMiddleware();

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
