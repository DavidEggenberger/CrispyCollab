using Shared.Modules.Layers.Application.CQRS.Command;
using Shared.Modules.Layers.Infrastructure.CQRS.Command;
using Stripe;

namespace Shared.Modules.SubscriptionModule.Layers.Application.Commands
{
    public class CreateSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
