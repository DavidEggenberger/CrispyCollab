using Infrastructure.Identity.Types.Enums;

namespace Infrastructure.Identity.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
        public string StripeSubscriptionId { get; set; }
        public DateTime PeriodEnd { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
