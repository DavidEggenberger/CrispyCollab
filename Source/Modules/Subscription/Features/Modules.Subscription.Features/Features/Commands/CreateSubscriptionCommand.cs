using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Commands
{
    public class CreateSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
