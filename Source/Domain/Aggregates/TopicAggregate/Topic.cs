using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.TopicAggregate
{
    [AggregateRoot]
    public class Topic : Entity
    {
        public string Name { get; set; }
        public string Goal { get; set; }
        public TopicType Type { get; set; }
        public TopicStatus Status { get; set; }
        public List<Approach> Approaches { get; set; }
    }
}
