using Shared.SharedKernel.DomainKernel.Tenant;
using Shared.Modules.Layers.Infrastructure.StripeIntegration.Configuration;

namespace Shared.Modules.Layers.Infrastructure.StripeIntegration.Services.Interfaces
{
    public interface IStripeSubscriptionService
    {
        StripeSubscriptionType GetSubscriptionType(SubscriptionPlanType subscriptionPlanType);
    }
}
