using Shared.Infrastructure.CQRS.Command;
using Stripe;

namespace SubscriptionModule.Server.Features.Commands
{
    public class UpdateSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
