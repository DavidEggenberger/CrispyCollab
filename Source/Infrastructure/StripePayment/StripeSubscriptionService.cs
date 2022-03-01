using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StripePayment
{
    public class StripeSubscriptionService
    {
        private readonly SubscriptionService subscriptionService;
        private readonly IConfiguration configuration;
        public StripeSubscriptionService(IConfiguration configuration)
        {
            this.subscriptionService = new SubscriptionService();
            this.configuration = configuration;
        }

        public async Task<string> CreateSubscriptionAsync(string name)
        {
            return (await subscriptionService.CreateAsync(new SubscriptionCreateOptions {  })).Id;
        }

        public async Task<string> GetSubscriptionType(string id)
        {
            return (await subscriptionService.GetAsync(id)).Id;
        }

        public List<SessionLineItemOptions> LoadSubscriptionsFromConfiguration()
        {
            return configuration.GetSection("SubscriptionPlans")
                .GetChildren()
                .Select(x => new SessionLineItemOptions { Amount = 1, Price = x.Value })
                .ToList();
        }
    }
}
