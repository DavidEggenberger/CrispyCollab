using Shared.Modules.Layers.Infrastructure.StripeIntegration.Configuration;
using SharedKernel.DomainKernel;

namespace Shared.Modules.Layers.Infrastructure.StripeIntegration.Services.Interfaces
{
    public interface IStripeSubscriptionService
    {
        StripeSubscriptionType GetSubscriptionType(SubscriptionPlanType subscriptionPlanType);
    }
}
