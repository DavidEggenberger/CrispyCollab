using Infrastructure.CQRS.Query;
using Modules.ChannelModule.Domain;
using System.Threading;
using Modules.ChannelModule.Infrastructure.EFCore;

namespace Application.ChannelAggregate
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
