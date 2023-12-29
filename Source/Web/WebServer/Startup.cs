using Shared.Features.EmailSender;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebServer.Modules.ModelValidation;
using WebServer.Modules.HostingInformation;
using Shared.Features.RedisCache;
using WebServer.Modules.Swagger;
using Shared.Features.MultiTenancy;
using Shared.Kernel.BuildingBlocks.Authorization;

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
                //endpoints.MapSignalR();
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
