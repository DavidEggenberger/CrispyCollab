using Domain.Aggregates.ChannelAggregate;
using Infrastructure.CQRS.Query;
using Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ChannelAggregate.Queries
{
    public class GetMessageByIdQuery : IQuery<Message>
    {
        public Guid Id { get; set; }
    }
    public class GetMessageByIdQueryHandler : IQueryHandler<GetChannelByIdQuery, Channel>
    {
        private readonly ApplicationDbContext applicationDbContext;
        public GetMessageByIdQueryHandler(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public Task<Channel> HandleAsync(GetChannelByIdQuery query, CancellationToken cancellation)
        {
            return applicationDbContext.Channels.SingleAsync(c => c.Id == query.Id);
        }
    }
}
