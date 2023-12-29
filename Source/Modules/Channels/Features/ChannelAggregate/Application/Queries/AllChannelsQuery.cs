using Shared.Features.CQRS.Query;

namespace Modules.Channels.Features.ChannelAggregate.Application.Queries
{
    public class AllChannelsQuery : IQuery<List<Channel>>
    {

    }
    public class AllChannelsQueryHandler : BaseQueryHandler<ChannelDbContext, Channel>, IQueryHandler<AllChannelsQuery, List<Channel>>
    {
        public AllChannelsQueryHandler(ChannelDbContext applicationDbContext) : base(applicationDbContext) { }
        public async Task<List<Channel>> HandleAsync(AllChannelsQuery query, CancellationToken cancellation)
        {
            return dbSet.ToList();
        }
    }
}
