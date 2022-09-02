using Infrastructure.StripePayments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StripePayments.Configuration
{
    public static class StripeSubscriptions
    {
        public static IEnumerable<StripeSubscription> GetAllStripeSubscriptions()
        {
            yield return new StripeSubscription
            {

            };

            yield return new StripeSubscription
            {

            };
        }
    }
}
