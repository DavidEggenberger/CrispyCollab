using Domain.Aggregates.ChannelAggregate.Events;
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
        public bool MessagesAreAnonymous { get; set; }

        private List<Message> messages = new List<Message>();
        public IReadOnlyCollection<Message> Messages => messages.AsReadOnly();
        private Channel() { }
        public Channel(string name, bool messagesAreAnonymous)
        {
            Name = name;
            MessagesAreAnonymous = messagesAreAnonymous;
        }
        public void AddMessage(Message message)
        {
            messages.Add(message);
            AddDomainEvent(new ChannelMessagesUpdatedEvent());
        }
        public void RemoveMessage(Message message)
        {
            messages.Remove(messages.Single(m => m.Id == message.Id));
            AddDomainEvent(new ChannelMessagesUpdatedEvent());
        }
        public void AddMessageVote(Message message, Vote vote)
        {
            messages.Single(m => m.Id == message.Id).AddVote(vote);
        }
        public void RemoveMessageVote(Message message, Vote vote)
        {
            messages.Single(m => m.Id == message.Id).RemoveVote(vote);
        }
        public void SetTopicTrigger(TopicTrigger topicTrigger)
        {
            AddDomainEvent(new TopicTriggerUpdatedEvent());
        }
    }
}
