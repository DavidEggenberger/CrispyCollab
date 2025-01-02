using Shared.Features.Messaging.Query;
using Shared.Features.Server;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Queries
{
    public class GetChannelById : Query<Channel>
    {
        public Guid Id { get; set; }
    }
    public class GetChannelQueryHandler : ServerExecutionBase<ChannelsModule>, IQueryHandler<GetChannelById, Channel>
    {
        public GetChannelQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<Channel> HandleAsync(GetChannelById query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
