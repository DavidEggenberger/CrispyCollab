using Stripe;
using SubscriptionModule.Server.Features.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionModule.Server.Features.Services
{
    public class StripeCustomerService : IStripeCustomerService
    {
        public Task<Customer> CreateStripeCustomerAsync(string userName, string emailAddress)
        {
            return new CustomerService().CreateAsync(new CustomerCreateOptions { Name = userName, Email = emailAddress });
        }

        public Task DeleteStripeCustomerAsync(string stripeCustomerId)
        {
            return new CustomerService().DeleteAsync(stripeCustomerId);
        }

        public Task<Customer> UpdateStripeCustomerAsync(string stripeCustomerId)
        {
            return new CustomerService().UpdateAsync(stripeCustomerId, new CustomerUpdateOptions { Name = stripeCustomerId });
        }
    }
}
