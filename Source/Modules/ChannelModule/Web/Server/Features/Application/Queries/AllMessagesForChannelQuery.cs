using Shared.Infrastructure.CQRS.Query;
using Modules.ChannelModule.Infrastructure.EFCore;
using ChannelModule.Server.Features.Domain;

namespace ChannelModule.Server.Features.Application.Queries
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
