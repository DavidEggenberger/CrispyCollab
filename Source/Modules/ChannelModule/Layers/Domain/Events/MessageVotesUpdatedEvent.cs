using SharedKernel.Kernel;

namespace Modules.ChannelModule.Domain.Events
{
    public class MessageVotesUpdatedEvent : IDomainEvent
    {
        public Guid TeamId { get; set; }
    }
}
