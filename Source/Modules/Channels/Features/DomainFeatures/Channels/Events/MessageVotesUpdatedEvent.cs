using Shared.Features.Domain;

namespace Modules.Channels.Features.DomainFeatures.Channels.Events
{
    public class MessageVotesUpdatedEvent : IDomainEvent
    {
        public Guid TeamId { get; set; }
    }
}
