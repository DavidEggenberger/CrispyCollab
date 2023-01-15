using SharedKernel.DomainKernel;
using SubscriptionModule.Server.Infrastructure.Configuration;

namespace SubscriptionModule.Server.Features.Services.Interfaces
{
    public interface IStripeSubscriptionService
    {
        StripeSubscriptionType GetSubscriptionType(SubscriptionPlanType subscriptionPlanType);
    }
}
