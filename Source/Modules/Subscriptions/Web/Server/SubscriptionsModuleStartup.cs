using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscriptions.Features.Infrastructure.Configuration;
using Modules.Subscriptions.Features.Infrastructure.EFCore;
using Shared.Features.Configuration;
using Shared.Features.EFCore;
using Shared.Features.Modules;
using Stripe;
using System.Reflection;

namespace Modules.Subscriptions.Web.Server
{
    public class SubscriptionsModuleStartup : IModuleStartup
    {
        public Assembly FeaturesAssembly { get; } = typeof(SubscriptionsModuleStartup).Assembly;
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:StripeKey"];

            services.RegisterDbContext<SubscriptionsDbContext>("subscriptions");
            services.RegisterConfiguration<SubscriptionsConfiguration, SubscriptionsConfigurationValidator>(configuration);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
