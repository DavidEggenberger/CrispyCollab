using Infrastructure.CQRS.Command;
using Infrastructure.StripeIntegration.Commands;
using System.Threading;

namespace Application.Infrastructure.StripePayments.CommandHandlers
{
    public class CreateSubscriptionCommandHandler : ICommandHandler<CreateSubscriptionCommand>
    {
        public Task HandleAsync(CreateSubscriptionCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
