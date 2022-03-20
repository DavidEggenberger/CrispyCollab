using Domain.Aggregates.ChannelAggregate.Events;
using Infrastructure.CQRS.DomainEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ChannelAggregate.EventHandlers
{
    public class MessagesChangedEventHandler : IDomainEventHandler<MessagesChangedEvent>
    {
        public Task HandleAsync(MessagesChangedEvent query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
