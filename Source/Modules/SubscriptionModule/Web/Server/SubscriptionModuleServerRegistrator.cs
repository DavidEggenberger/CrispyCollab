using Shared.Modules.Layers.Infrastructure.EmailSender;
using Shared.Modules.Layers.Infrastructure.StripeIntegration.Configuration;
using Shared.Modules.Layers.Infrastructure.StripeIntegration.Services;
using Shared.Modules.Layers.Infrastructure.StripeIntegration.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using SubscriptionModule.Server.Infrastructure.Configuration;

namespace Shared.Modules.Layers.Infrastructure.StripeIntegration
{
    public static class StripeDIRegistrator
    {
        public static IServiceCollection RegisterStripe(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:StripeKey"];
            services.Configure<StripeOptions>(configuration);
            services.AddScoped<IStripeSubscriptionService, StripeSubscriptionTypeService>();

            return services;
        }
    }
}
