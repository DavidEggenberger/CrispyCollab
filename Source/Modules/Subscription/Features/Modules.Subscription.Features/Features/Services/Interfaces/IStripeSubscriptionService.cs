using Modules.Subscriptions.Features.Configuration;

namespace Modules.Subscriptions.Features.Services.Interfaces
{
    public interface IStripeSubscriptionService
    {
        StripeSubscriptionType GetSubscriptionType(SubscriptionPlanType subscriptionPlanType);
    }
}
