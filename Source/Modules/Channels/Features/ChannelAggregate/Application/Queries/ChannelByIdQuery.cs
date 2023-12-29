using Shared.Features.CQRS.Query;
using Microsoft.EntityFrameworkCore;

namespace Modules.Channels.Features.ChannelAggregate.Application.Queries
{
    public class ChannelByIdQuery : IQuery<Channel>
    {
        public Guid Id { get; set; }
    }
    public class GetChannelQueryHandler : BaseQueryHandler<ChannelDbContext, Channel>, IQueryHandler<ChannelByIdQuery, Channel>
    {
        public GetChannelQueryHandler(ChannelDbContext applicationDbContext) : base(applicationDbContext) { }
        public Task<Channel> HandleAsync(ChannelByIdQuery query, CancellationToken cancellation)
        {
            return dbSet.SingleAsync(c => c.Id == query.Id);
        }
    }
}
