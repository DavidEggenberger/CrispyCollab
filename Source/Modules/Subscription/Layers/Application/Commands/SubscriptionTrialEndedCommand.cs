using Shared.Infrastructure.CQRS.Command;
using Stripe;

namespace Shared.Modules.SubscriptionModule.Layers.Application.Commands
{
    public class SubscriptionTrialEndedCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
