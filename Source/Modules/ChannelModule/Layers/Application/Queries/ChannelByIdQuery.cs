using Modules.ChannelModule.Domain;
using Infrastructure.CQRS.Query;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Modules.ChannelModule.Domain;
using System.Threading;
using Modules.ChannelModule.Infrastructure.EFCore;

namespace Application.ChannelAggregate.Queries
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
