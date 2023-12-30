using Modules.Channels.Features.Aggregates.ChannelAggregate.Events;

namespace Modules.Channels.Features.Aggregates.ChannelAggregate.Application.EventHandlers
{
    public class ChannelMessagesUpdatedEventHandler : IDomainEventHandler<MessageVotesUpdatedEvent>
    {
        public Task HandleAsync(MessageVotesUpdatedEvent query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
