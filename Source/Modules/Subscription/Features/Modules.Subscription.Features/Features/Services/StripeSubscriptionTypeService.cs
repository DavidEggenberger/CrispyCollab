using Modules.Subscriptions.Features.Services.Interfaces;
using Modules.Subscriptions.Features.Configuration;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;

namespace Modules.Subscriptions.Features.Services
{
    public class StripeSubscriptionTypeService : IStripeSubscriptionService
    {
        private List<StripeSubscriptionType> subscriptions;
        public StripeSubscriptionTypeService()
        {
            subscriptions = new List<StripeSubscriptionType>()
            {
                new StripeSubscriptionType
                {
                    Type = SubscriptionPlanType.Professional,
                    TrialPeriodDays = 7,
                    StripePriceId = "price_1KYcx5EhLcfJYVVFmQGwWRdb"
                },
                new StripeSubscriptionType
                {
                    Type = SubscriptionPlanType.Enterprise,
                    TrialPeriodDays = 7,
                    StripePriceId = "price_1KYd0eEhLcfJYVVF7YWjH5Cf"
                }
            };
        }
        public StripeSubscriptionType GetSubscriptionType(SubscriptionPlanType subscriptionPlanType)
        {
            return subscriptions.Single(s => s.Type == subscriptionPlanType);
        }
    }
}
