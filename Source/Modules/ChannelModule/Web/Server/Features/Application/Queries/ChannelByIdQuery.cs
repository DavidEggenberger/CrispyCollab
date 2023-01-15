using Shared.Infrastructure.CQRS.Query;
using Microsoft.EntityFrameworkCore;
using Modules.ChannelModule.Infrastructure.EFCore;
using ChannelModule.Server.Features.Domain;

namespace ChannelModule.Server.Features.Application.Queries
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
