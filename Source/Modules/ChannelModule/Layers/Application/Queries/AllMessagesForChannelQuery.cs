using Infrastructure.CQRS.Query;
using Modules.ChannelModule.Domain;
using System.Threading;
using Modules.ChannelModule.Infrastructure.EFCore;

namespace Application.ChannelAggregate
{
    public class AllMessagesForChannel : IQuery<List<Channel>> 
    { 

    }
    public class AllMessagesForChannelQueryHandler : BaseQueryHandler<ChannelDbContext, Channel>, IQueryHandler<AllChannelsQuery, List<Channel>>
    {
        public AllMessagesForChannelQueryHandler(ChannelDbContext applicationDbContext) : base(applicationDbContext) { }
        public async Task<List<Channel>> HandleAsync(AllChannelsQuery query, CancellationToken cancellation)
        {
            return dbSet.ToList();
        }
    }
}
