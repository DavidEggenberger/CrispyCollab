using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Query;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Queries
{
    public class AllMessagesForChannel : Query<List<Channel>>
    {

    }
    public class AllMessagesForChannelQueryHandler : BaseQueryHandler<ChannelsDbContext, Channel>, IQueryHandler<GetAllChannels, List<Channel>>
    {
        public AllMessagesForChannelQueryHandler(ChannelsDbContext applicationDbContext) : base(applicationDbContext) { }
        public async Task<List<Channel>> HandleAsync(GetAllChannels query, CancellationToken cancellation)
        {
            return dbSet.ToList();
        }
    }
}
