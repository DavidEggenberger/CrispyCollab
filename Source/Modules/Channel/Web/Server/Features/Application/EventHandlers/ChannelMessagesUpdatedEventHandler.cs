using ChannelModule.Server.Features.Domain.Events;
using Shared.Infrastructure.CQRS.DomainEvent;

namespace ChannelModule.Server.Features.Application.EventHandlers
{
    public class ChannelMessagesUpdatedEventHandler : IDomainEventHandler<MessageVotesUpdatedEvent>
    {
        public Task HandleAsync(MessageVotesUpdatedEvent query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
