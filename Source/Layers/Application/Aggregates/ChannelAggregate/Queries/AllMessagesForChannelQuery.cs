using Infrastructure.CQRS.Query;
using Domain.Aggregates.ChannelAggregate;
using System.Threading;
using Infrastructure.EFCore;

namespace Application.ChannelAggregate
{
    public class AllMessagesForChannel : IQuery<List<Channel>> 
    { 

    }
    public class AllMessagesForChannelQueryHandler : BaseQueryHandler<Channel>, IQueryHandler<AllChannelsQuery, List<Channel>>
    {
        public AllMessagesForChannelQueryHandler(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
        public async Task<List<Channel>> HandleAsync(AllChannelsQuery query, CancellationToken cancellation)
        {
            return dbSet.ToList();
        }
    }
}
