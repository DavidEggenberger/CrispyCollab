using Shared.Features.CQRS.Query;

namespace Modules.Channels.Features.ChannelAggregate.Application.Queries
{
    public class MessageByIdQuery : IQuery<Message>
    {
        public Guid Id { get; set; }
    }
}
