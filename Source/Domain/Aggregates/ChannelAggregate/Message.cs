using Domain.Kernel;
using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ChannelAggregate
{
    public class Message : Entity
    {
        public DateTime TimeSent { get; set; }
        public string Text { get; set; }
        public bool HasDerivedTopic { get; set; }
        public string DerivedTopicId { get; set; }
        public List<Vote> MakeTopicVotes { get; set; }
    }
}
