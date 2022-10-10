using Infrastructure.CQRS.Command;
using Stripe;

namespace Infrastructure.StripeIntegration.Commands
{
    public class SubscriptionTrialEndedCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
