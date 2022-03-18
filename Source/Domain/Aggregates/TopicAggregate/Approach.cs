using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.TopicAggregate
{
    public class Approach : Entity
    {
        public Guid TopicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ApproachStatus Status { get; set; }
        public List<Reaction> Reactions { get; set; }
    }
}
