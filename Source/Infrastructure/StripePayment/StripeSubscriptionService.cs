using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StripePayment
{
    public class StripeSubscriptionService
    {
        private SubscriptionService subscriptionService;
        public StripeSubscriptionService(SubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
        }

        public async Task<string> CreateSubscriptionAsync(string name)
        {
            return (await subscriptionService.CreateAsync(new SubscriptionCreateOptions {  })).Id;
        }

        public async Task<string> GetSubscriptionType(string id)
        {
            return (await subscriptionService.GetAsync(id)).Id;
        }
    }
}
