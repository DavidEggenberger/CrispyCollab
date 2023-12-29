using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Modules.LandingPages.Server
{
    public class LandingPagesModuleStartup : IModuleStartup
    {
        public Assembly FeaturesAssembly { get; } = typeof(LandingPagesModuleStartup).Assembly;
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages()
                .AddApplicationPart(typeof(LandingPagesModuleStartup).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
