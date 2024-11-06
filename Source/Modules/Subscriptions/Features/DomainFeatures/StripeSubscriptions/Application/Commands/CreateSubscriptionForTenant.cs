using Shared.Features.Messaging.Command;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.Application.Commands
{
    public class CreateSubscriptionForTenant : ICommand
    {
        public Guid TenantId { get; set; }
        public Stripe.Subscription Subscription { get; set; }
    }
}
