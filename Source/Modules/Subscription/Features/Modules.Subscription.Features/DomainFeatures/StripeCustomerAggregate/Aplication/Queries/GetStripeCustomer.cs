using Modules.Subscriptions.Features.DomainFeatures.StripeCustomerAggregate;
using Shared.Features.CQRS.Query;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomerAggregate.Aplication.Queries
{
    public class GetStripeCustomer : IQuery<StripeCustomer>
    {
        public string StripeCustomerId { get; set; }
    }
}
