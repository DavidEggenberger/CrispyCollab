using Domain.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ChannelAggregate
{
    public record MakeTopicVote : ValueObject
    {
        public bool MakeTopic { get; set; }
    }
}
