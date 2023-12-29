using Stripe;

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
