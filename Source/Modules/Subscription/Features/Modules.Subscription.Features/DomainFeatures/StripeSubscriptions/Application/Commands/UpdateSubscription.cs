using Shared.Features.Messaging.Command;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.Application.Commands
{
    public class UpdateSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
