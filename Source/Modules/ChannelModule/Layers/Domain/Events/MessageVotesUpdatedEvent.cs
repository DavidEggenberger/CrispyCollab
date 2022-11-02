using Shared.Modules.Layers.Domain.Interfaces;
using Shared.SharedKernel.Interfaces;

namespace Modules.ChannelModule.Domain.Events
{
    public class MessageVotesUpdatedEvent : IDomainEvent
    {
        public Guid TeamId { get; set; }
    }
}
