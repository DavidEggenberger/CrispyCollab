using Modules.Subscriptions.Features.DomainFeatures.StripeCustomerAggregate;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate
{
    public class StripeSubscription : AggregateRoot
    {
        public StripeCustomer StripeCustomer { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SubscriptionPlanType PlanType { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
