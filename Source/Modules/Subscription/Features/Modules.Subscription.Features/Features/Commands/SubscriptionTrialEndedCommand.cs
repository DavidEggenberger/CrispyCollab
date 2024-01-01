using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Commands
{
    public class SubscriptionTrialEndedCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
