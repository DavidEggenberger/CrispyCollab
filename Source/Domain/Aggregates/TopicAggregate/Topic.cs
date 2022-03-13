using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.SubjectAggregate
{
    [AggregateRoot]
    public class Topic
    {
        public string Name { get; set; }
    }
}
