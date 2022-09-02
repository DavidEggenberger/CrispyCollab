using Domain.Aggregates.TenantAggregate;
using Infrastructure.Identity;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.StripePayments.Models
{
    public class StripeSubscription
    {
        public Subscription Subscription { get; set; }
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
        public int TrialPeriodDays { get; set; }
    }
}
