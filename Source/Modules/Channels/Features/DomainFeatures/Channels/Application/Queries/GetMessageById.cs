using Shared.Features.Messaging.Query;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Queries
{
    public class GetMessageById : Query<Message>
    {
        public Guid Id { get; set; }
    }
}
