using Shared.Kernel.DomainKernel;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions
{
    public class StripeSubscriptionPlan
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
    }
}
