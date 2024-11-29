using Shared.Features.Domain;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomers
{
    public class StripeCustomer : Entity
    {
        public string StripePortalCustomerId { get; set; }
    }
}
