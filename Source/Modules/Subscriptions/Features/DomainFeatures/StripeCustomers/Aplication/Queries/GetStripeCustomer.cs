using Shared.Features.Messaging.Query;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomers.Aplication.Queries
{
    public class GetStripeCustomer : Query<StripeCustomer>
    {
        public string StripeCustomerId { get; set; }
    }
}
