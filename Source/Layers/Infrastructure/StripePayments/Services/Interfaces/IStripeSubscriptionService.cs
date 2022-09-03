using Infrastructure.Identity;
using Infrastructure.StripePayments.Models;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StripePayments.Services.Interfaces
{
    public interface IStripeSubscriptionService
    {
        StripeSubscription GetSubscriptionFromPlanType(SubscriptionPlanType subscriptionPlanType);
    }
}
