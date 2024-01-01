using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Commands
{
    public class DeleteSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
