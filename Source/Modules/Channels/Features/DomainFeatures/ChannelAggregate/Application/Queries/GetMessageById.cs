using Shared.Features.CQRS.Query;

namespace Modules.Channels.Features.DomainFeatures.ChannelAggregate.Application.Queries
{
    public class GetMessageById : IQuery<Message>
    {
        public Guid Id { get; set; }
    }
}
