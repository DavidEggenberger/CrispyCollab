using Infrastructure.CQRS.Query;
using Domain.Aggregates.ChannelAggregate;
using System.Threading;
using Infrastructure.EFCore;

namespace Application.ChannelAggregate
{
    public class AllChannelsQuery : IQuery<List<Channel>> 
    { 

    }
    public class AllChannelsQueryHandler : BaseQueryHandler<Channel>, IQueryHandler<AllChannelsQuery, List<Channel>>
    {
        public AllChannelsQueryHandler(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
        public async Task<List<Channel>> HandleAsync(AllChannelsQuery query, CancellationToken cancellation)
        {
            return dbSet.ToList();
        }
    }
}
