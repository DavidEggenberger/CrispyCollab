using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.TopicAggregate
{
    public class Approach
    {
        public Guid TopicId { get; set; }
        public Guid UserId { get; set; }
        public ApproachStatus Status { get; set; }
    }
}
