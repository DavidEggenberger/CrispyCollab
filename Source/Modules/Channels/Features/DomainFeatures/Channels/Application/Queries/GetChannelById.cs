using Microsoft.EntityFrameworkCore;
using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Query;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Queries
{
    public class GetChannelById : Query<Channel>
    {
        public Guid Id { get; set; }
    }
    public class GetChannelQueryHandler : BaseQueryHandler<ChannelsDbContext, Channel>, IQueryHandler<GetChannelById, Channel>
    {
        public GetChannelQueryHandler(ChannelsDbContext applicationDbContext) : base(applicationDbContext) { }
        public Task<Channel> HandleAsync(GetChannelById query, CancellationToken cancellation)
        {
            return dbSet.SingleAsync(c => c.Id == query.Id);
        }
    }
}
