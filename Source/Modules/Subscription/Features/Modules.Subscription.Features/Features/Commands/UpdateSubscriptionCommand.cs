using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Commands
{
    public class UpdateSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
