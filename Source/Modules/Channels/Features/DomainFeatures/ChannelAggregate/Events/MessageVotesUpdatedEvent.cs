using Shared.Features.Domain;

namespace Modules.Channels.Features.DomainFeatures.ChannelAggregate.Events
{
    public class MessageVotesUpdatedEvent : IDomainEvent
    {
        public Guid TeamId { get; set; }
    }
}
