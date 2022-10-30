using Shared.Modules.Layers.Application.CQRS.Query;
using Modules.ChannelModule.Domain;
using Shared.Modules.Layers.Infrastructure.CQRS.Query;
using Modules.ChannelModule.Infrastructure.EFCore;

namespace Modules.ChannelModule.Layers.Application.Queries
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
