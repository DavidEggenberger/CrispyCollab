using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace Infrastructure.Stripe
{
    public static class StripeDIRegistrator
    {
        public static IServiceCollection RegisterStripe(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["StripeKey"];
            //services.AddScoped<SubscriptionManager>();
            //services.AddScoped<SubscriptionPlanManager>();

            return services;
        }
    }
}
