using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Modules.Layers.Infrastructure.StripeIntegration.Services.Interfaces
{
    public interface IStripeCustomerService
    {
        Task<Customer> CreateStripeCustomerAsync(string userName, string emailAddress);
        Task DeleteStripeCustomerAsync(string stripeCustomerId);
        Task<Customer> UpdateStripeCustomerAsync(string stripeCustomerId);
    }
}
