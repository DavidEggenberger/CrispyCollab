using Shared.Modules.Layers.Features.EmailSender;
using Shared.Modules.Layers.Features.StripeIntegration.Configuration;
using Shared.Modules.Layers.Features.StripeIntegration.Services;
using Shared.Modules.Layers.Features.StripeIntegration.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using SubscriptionModule.Server.Features.Configuration;

namespace Shared.Modules.Layers.Features.StripeIntegration
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
