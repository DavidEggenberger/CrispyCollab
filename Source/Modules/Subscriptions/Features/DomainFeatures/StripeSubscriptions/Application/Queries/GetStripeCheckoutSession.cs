using Shared.Features.Messaging.Query;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.Application.Queries
{
    public class GetStripeCheckoutSession : Query<Stripe.Checkout.Session>
    {
        public string SessionId { get; set; }
    }
}
