using SubscriptionModule.Server.Features.Configuration;

namespace SubscriptionModule.Server.Features.Services.Interfaces
{
    public interface IStripeSubscriptionService
    {
        StripeSubscriptionType GetSubscriptionType(SubscriptionPlanType subscriptionPlanType);
    }
}
