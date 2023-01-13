using Modules.ChannelModule.Domain.Events;
using Shared.Infrastructure.CQRS.DomainEvent;

namespace Modules.ChannelModule.Layers.Application.EventHandlers
{
    public class ChannelMessagesUpdatedEventHandler : IDomainEventHandler<MessageVotesUpdatedEvent>
    {
        public Task HandleAsync(MessageVotesUpdatedEvent query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
