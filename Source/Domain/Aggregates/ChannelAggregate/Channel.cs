using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ChannelAggregate
{
    [AggregateRoot]
    public class Channel
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public List<Message> MyProperty { get; set; }
    }
}
