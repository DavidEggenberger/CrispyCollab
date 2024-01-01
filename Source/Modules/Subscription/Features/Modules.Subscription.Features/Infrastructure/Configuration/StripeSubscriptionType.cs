using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;

namespace Modules.Subscriptions.Features.Configuration
{
    public class StripeSubscriptionType
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
        public int TrialPeriodDays { get; set; }
    }
}
