using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.TriggerAggregate
{
    [AggregateRoot]
    public class Trigger
    {
        public string Name { get; set; }
        public TriggerType Type { get; set; }
        public bool Triggered { get; set; }
        public List<Condition> Conditions { get; set; }
    }
}
