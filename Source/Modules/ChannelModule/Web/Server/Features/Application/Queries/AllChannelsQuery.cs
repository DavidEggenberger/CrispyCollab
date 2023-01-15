﻿using Shared.Infrastructure.CQRS.Query;
using Modules.ChannelModule.Infrastructure.EFCore;
using ChannelModule.Server.Features.Domain;

namespace ChannelModule.Server.Features.Application.Queries
{
    public class AllChannelsQuery : IQuery<List<Channel>>
    {

    }
    public class AllChannelsQueryHandler : BaseQueryHandler<ChannelDbContext, Channel>, IQueryHandler<AllChannelsQuery, List<Channel>>
    {
        public AllChannelsQueryHandler(ChannelDbContext applicationDbContext) : base(applicationDbContext) { }
        public async Task<List<Channel>> HandleAsync(AllChannelsQuery query, CancellationToken cancellation)
        {
            return dbSet.ToList();
        }
    }
}