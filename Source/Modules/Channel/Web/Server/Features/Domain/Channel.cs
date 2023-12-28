using ChannelModule.Server.Features.Domain.Events;
using Shared.Domain;
using Shared.Domain.Attributes;

namespace ChannelModule.Server.Features.Domain
{
    [AggregateRoot]
    public class Channel : Entity
    {
        public string Name { get; set; }
        public string Goal { get; set; }
        public bool MessagesSenderIsAonymous { get; set; }

        private List<Message> messages = new List<Message>();
        public IReadOnlyCollection<Message> Messages => messages.AsReadOnly();
        private Channel() { }
        public Channel(string name, string goal, bool messagesAreAnonymous)
        {
            Name = name;
            Goal = goal;
            MessagesSenderIsAonymous = messagesAreAnonymous;
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
        public void AddMessageVote(Message message, Reaction vote)
        {
            messages.Single(m => m.Id == message.Id).AddVote(vote);
        }
        public void RemoveMessageVote(Message message, Reaction vote)
        {
            messages.Single(m => m.Id == message.Id).RemoveVote(vote);
        }
    }
}
