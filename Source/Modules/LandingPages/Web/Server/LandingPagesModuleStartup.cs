using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.Modules;
using System.Reflection;
using Blazored.Modal;

namespace Modules.LandingPages.Server
{
    public class LandingPagesModuleStartup : IModuleStartup
    {
        public Assembly FeaturesAssembly { get; } = typeof(LandingPagesModuleStartup).Assembly;
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddBlazoredModal();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
