using Shared.Modules.Layers.Application.CQRS.DomainEvent;
using Modules.ChannelModule.Domain.Events;

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
