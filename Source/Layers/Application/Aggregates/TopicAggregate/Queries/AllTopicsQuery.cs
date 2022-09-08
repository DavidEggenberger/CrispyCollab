using Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates;
using Domain.Aggregates.TopicAggregate;

namespace Application.TopicAggregate.Queries
{
    public class AllTopicsQuery : IQuery<List<Topic>>
    {
    }
}
