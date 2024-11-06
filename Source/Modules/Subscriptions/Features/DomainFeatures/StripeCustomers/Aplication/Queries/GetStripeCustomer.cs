using Shared.Features.Messaging.Query;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomers.Aplication.Queries
{
    public class GetStripeCustomer : IQuery<StripeCustomer>
    {
        public string StripeCustomerId { get; set; }
    }
}
