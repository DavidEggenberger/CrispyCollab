using SharedKernel.DomainKernel.Tenant;
using Infrastructure.StripeIntegration.Configuration;

namespace Infrastructure.StripeIntegration.Services.Interfaces
{
    public interface IStripeSubscriptionService
    {
        StripeSubscriptionType GetSubscriptionType(SubscriptionPlanType subscriptionPlanType);
    }
}
