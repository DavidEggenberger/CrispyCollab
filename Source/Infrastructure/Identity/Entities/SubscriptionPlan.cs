using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class SubscriptionPlan
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public string StripePriceId { get; set; }
        public int TrialPeriodDays { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
    }
}
