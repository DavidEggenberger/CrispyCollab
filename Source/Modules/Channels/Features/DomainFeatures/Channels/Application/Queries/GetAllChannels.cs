using Shared.Features.Messaging.Query;
using Shared.Features.Server;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Queries
{
    public class GetAllChannels : Query<List<Channel>>
    {

    }
    public class AllChannelsQueryHandler : ServerExecutionBase<ChannelsModule>, IQueryHandler<GetAllChannels, List<Channel>>
    {
        public AllChannelsQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<List<Channel>> HandleAsync(GetAllChannels query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
