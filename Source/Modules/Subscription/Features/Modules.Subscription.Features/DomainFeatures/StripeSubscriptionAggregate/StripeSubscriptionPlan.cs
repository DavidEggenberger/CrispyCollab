using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.Subscriptions.Features.DomainFeatures.SubscriptionAggregate
{
    public class StripeSubscriptionPlan
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
    }
}
