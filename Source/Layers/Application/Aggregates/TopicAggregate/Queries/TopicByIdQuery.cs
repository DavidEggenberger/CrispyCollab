using Domain.Aggregates.TopicAggregate;
using Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TopicAggregate.Queries
{
    public class TopicByIdQuery : IQuery<Topic>
    {
    }
}
