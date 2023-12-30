using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.Modules;
using System.Reflection;
using Shared.Features.EFCore;

namespace Modules.Channels.Web.Server
{
    public class ChannelsModuleStartup : IModuleStartup
    {
        public Assembly FeaturesAssembly { get; } = typeof(ChannelsModuleStartup).Assembly;
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(ChannelsModuleStartup).Assembly);

            services.RegisterDbContext<ChannelsDbContext>("Channels");
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            
        }
    }
}
