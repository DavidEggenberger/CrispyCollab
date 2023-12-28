using Modules.ChannelModule.Domain;
using Shared.Infrastructure.CQRS.Query;

namespace Modules.ChannelModule.Layers.Application.Queries
{
    public class MessageByIdQuery : IQuery<Message>
    {
        public Guid Id { get; set; }
    }
}
