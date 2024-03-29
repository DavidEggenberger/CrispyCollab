using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            //services.Configure<StripeOptions>(configuration);
            //services.AddScoped<IStripeSubscriptionService, StripeSubscriptionTypeService>();

            services.AddControllers()
                .AddApplicationPart(typeof(SubscriptionsModuleStartup).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
