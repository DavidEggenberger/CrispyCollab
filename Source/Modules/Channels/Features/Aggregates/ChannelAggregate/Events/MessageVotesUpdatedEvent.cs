namespace Modules.Channels.Features.Aggregates.ChannelAggregate.Events
{
    public class MessageVotesUpdatedEvent : IDomainEvent
    {
        public Guid TeamId { get; set; }
    }
}
