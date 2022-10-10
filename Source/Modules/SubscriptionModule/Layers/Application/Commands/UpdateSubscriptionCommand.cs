using Infrastructure.CQRS.Command;
using Stripe;

namespace Infrastructure.StripeIntegration.Commands
{
    public class UpdateSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
