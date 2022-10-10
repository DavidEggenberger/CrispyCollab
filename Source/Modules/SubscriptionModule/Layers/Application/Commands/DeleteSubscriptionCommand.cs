using Infrastructure.CQRS.Command;
using Stripe;

namespace Infrastructure.StripeIntegration.Commands
{
    public class DeleteSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
