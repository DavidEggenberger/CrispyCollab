using Modules.Channels.Features.DomainFeatures.Channels.Events;
using Shared.Features.Messaging.DomainEvent;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.EventHandlers
{
    public class ChannelMessagesUpdatedEventHandler : IDomainEventHandler<MessageVotesUpdatedEvent>
    {
        public Task HandleAsync(MessageVotesUpdatedEvent query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
