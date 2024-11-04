using Shared.Features.Domain;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomers
{
    public class StripeCustomer : AggregateRoot
    {
        public Guid UserId { get; set; }
        public string StripePortalCustomerId { get; set; }
    }
}
