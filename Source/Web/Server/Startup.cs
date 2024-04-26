using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.Modules;
using Modules.Channels.Web.Server;
using Modules.LandingPages.Server;
using Modules.Subscriptions.Web.Server;
using Modules.TenantIdentity.Web.Server;
using Web.Server.BuildingBlocks;
using Modules.Subscriptions.Features;
using Modules.TenantIdentity.Features;
using Shared.Features;

namespace Web.Server
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
            services.AddControllers();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSharedFeatures();
            services.AddBuildingBlocks();

            services.AddModule<ChannelsModuleStartup>(Configuration);
            services.AddModule<LandingPagesModuleStartup>(Configuration);
            services.AddModule<SubscriptionsModule, SubscriptionsModuleStartup>(Configuration);
            services.AddModule<TenantIdentityModule, TenantIdentityModuleStartup>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSharedFeaturesMiddleware(env);
            app.UseBuildingBlocks();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
