using Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.ChannelAggregate;

namespace Application.ChannelAggregate
{
    public class GetAllChannelsQuery : IQuery<List<Channel>> { }
    public class GetAllChannelsQueryHandler
    {

    }
}
