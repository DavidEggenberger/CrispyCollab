using Domain.Aggregates.MessagingAggregate;
using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ChannelAggregate
{
    [AggregateRoot]
    public class Channel : Entity
    {
        public string Name { get; set; }
        public bool IsAnonymous { get; set; }

        private List<Message> messages = new List<Message>();
        public IReadOnlyCollection<Message> Messages => messages.AsReadOnly();
        public void AddMessage(Message message)
        {

        }
        public void RemoveMessage(Message message)
        {

        }
    }
}
