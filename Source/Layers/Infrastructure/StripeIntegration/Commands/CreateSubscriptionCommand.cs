using Infrastructure.CQRS.Command;
using Stripe;

namespace Infrastructure.StripeIntegration.Commands
{
    public class CreateSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
