using Shared.Features.CQRS.Query;
using Microsoft.EntityFrameworkCore;
using Modules.Channels.Features.Aggregates.ChannelAggregate;
using Modules.Channels.Features.Infrastructure.EFCore;

namespace Modules.Channels.Features.Aggregates.ChannelAggregate.Application.Queries
{
    public class GetChannelById : IQuery<Channel>
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
