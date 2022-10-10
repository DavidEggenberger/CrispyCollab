using Infrastructure.EmailSender;
using Infrastructure.StripeIntegration.Configuration;
using Infrastructure.StripeIntegration.Services;
using Infrastructure.StripeIntegration.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace Infrastructure.StripeIntegration
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
