using Domain.Aggregates.ChannelAggregate;
using Infrastructure.CQRS.Query;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Application.ChannelAggregate.Queries
{
    public class ChannelByIdQuery : IQuery<Channel> 
    {
        public Guid Id { get; set; }
    }
    public class GetChannelQueryHandler : BaseQueryHandler<Channel>, IQueryHandler<ChannelByIdQuery, Channel>
    {
        public GetChannelQueryHandler(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
        public Task<Channel> HandleAsync(ChannelByIdQuery query, CancellationToken cancellation)
        {
            return dbSet.SingleAsync(c => c.Id == query.Id);
        }
    }
}
