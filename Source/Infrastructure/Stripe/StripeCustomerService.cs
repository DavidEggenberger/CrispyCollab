using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Stripe
{
    public class StripeCustomerService
    {
        private CustomerService customerService;
        public StripeCustomerService()
        {
            customerService = new CustomerService();
        }

        /// <summary>
        /// Return the Id of the created user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<string> CreateStripeCustomer(string email)
        {
            return (await customerService.CreateAsync(new CustomerCreateOptions { Email = email })).Id;
        }


    }
}
