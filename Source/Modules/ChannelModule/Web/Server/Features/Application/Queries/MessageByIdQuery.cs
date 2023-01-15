using ChannelModule.Server.Features.Domain;
using Shared.Infrastructure.CQRS.Query;

namespace ChannelModule.Server.Features.Application.Queries
{
    public class MessageByIdQuery : IQuery<Message>
    {
        public Guid Id { get; set; }
    }
}
