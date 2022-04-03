using Domain.Kernel;
using Domain.Shared;
using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.TopicAggregate
{
    public class Reaction : ValueObject
    {
        public ReactionType Type { get; set; }
    }
}
