using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Query;

namespace Modules.Channels.Features.DomainFeatures.ChannelAggregate.Application.Queries
{
    public class AllMessagesForChannel : IQuery<List<Channel>>
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
