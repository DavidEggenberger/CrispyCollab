using Shared.Features.Domain;
using Shared.Features.DomainKernel;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomerAggregate
{
    public class StripeCustomer : AggregateRoot
    {
        public Guid UserId { get; set; }
        public string StripePortalCustomerId { get; set; }
    }
}
