using Domain.Aggregates.TenantAggregate.Enums;
using Infrastructure.StripeIntegration.Configuration;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StripeIntegration.Services.Interfaces
{
    public interface IStripeSubscriptionService
    {
        StripeSubscriptionType GetSubscriptionType(SubscriptionPlanType subscriptionPlanType);
    }
}
