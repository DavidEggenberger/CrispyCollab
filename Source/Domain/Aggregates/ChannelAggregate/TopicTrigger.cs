using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ChannelAggregate
{
    public class TopicTrigger : Entity
    {
        public decimal NeccessaryVoteRelativePercentageCount { get; set; }
        public int NeccessaryVoteAbsoluteCount { get; set; }
    }
}
