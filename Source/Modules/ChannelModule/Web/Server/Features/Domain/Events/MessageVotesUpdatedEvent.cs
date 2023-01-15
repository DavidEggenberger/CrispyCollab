using Shared.Domain.Interfaces;
using Shared.SharedKernel.Interfaces;

namespace ChannelModule.Server.Features.Domain.Events
{
    public class MessageVotesUpdatedEvent : IDomainEvent
    {
        public Guid TeamId { get; set; }
    }
}
