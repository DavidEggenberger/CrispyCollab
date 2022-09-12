using Domain.Aggregates.TenantAggregate;
using Infrastructure.Identity;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StripeIntegration.Configuration
{
    public class StripeSubscriptionType
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
        public int TrialPeriodDays { get; set; }
    }
}
