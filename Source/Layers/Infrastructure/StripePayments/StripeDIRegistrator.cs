using Infrastructure.EmailSender;
using Infrastructure.StripePayments.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace Infrastructure.StripePayments
{
    public static class StripeDIRegistrator
    {
        public static IServiceCollection RegisterStripe(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:StripeKey"];
            services.Configure<StripeOptions>(configuration);

            return services;
        }
    }
}
