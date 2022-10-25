using Shared.Modules.Layers.Application.CQRS.DomainEvent;
using Modules.ChannelModule.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
