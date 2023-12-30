using Shared.Features.CQRS.Command;

namespace SubscriptionModule.Server.Features.Commands
{
    public class CreateSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
