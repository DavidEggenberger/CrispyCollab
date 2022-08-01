using Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Aggregates.ChannelAggregate;
using System.Threading;
using Infrastructure.EFCore;

namespace Application.ChannelAggregate
{
    public class AllChannelsQuery : IQuery<List<Channel>> { }
    public class AllChannelsQueryHandler : IQueryHandler<AllChannelsQuery, List<Channel>>
    {
        private readonly ApplicationDbContext applicationDbContext;
        public AllChannelsQueryHandler(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<List<Channel>> HandleAsync(AllChannelsQuery query, CancellationToken cancellation)
        {
            return applicationDbContext.Channels.ToList();
        }
    }
}
