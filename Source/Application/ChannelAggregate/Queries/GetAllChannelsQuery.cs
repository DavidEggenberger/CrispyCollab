using Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.ChannelAggregate;
using System.Threading;
using Infrastructure.EFCore;

namespace Application.ChannelAggregate
{
    public class GetAllChannelsQuery : IQuery<List<Channel>> { }
    public class GetAllChannelsQueryHandler : IQueryHandler<GetAllChannelsQuery, List<Channel>>
    {
        private readonly ApplicationDbContext applicationDbContext;
        public GetAllChannelsQueryHandler(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<List<Channel>> HandleAsync(GetAllChannelsQuery query, CancellationToken cancellation)
        {
            return applicationDbContext.Channels.ToList();
        }
    }
}
