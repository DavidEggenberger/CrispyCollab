using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ChannelAggregate.Events
{
    public class MessagesChangedEvent : IDomainEvent
    {
        public Guid TeamId { get; set; }
    }
}
