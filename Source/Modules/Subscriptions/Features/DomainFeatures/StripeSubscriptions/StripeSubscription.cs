using Modules.Subscriptions.Features.DomainFeatures.StripeCustomers;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions
{
    public class StripeSubscription : AggregateRoot
    {
        public StripeCustomer StripeCustomer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
