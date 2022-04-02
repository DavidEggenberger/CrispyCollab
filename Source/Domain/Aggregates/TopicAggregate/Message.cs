using Domain.Aggregates.TopicAggregate;
using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.TopicAggregate
{
    public class Message : Entity
    {
        public Guid ApproachId { get; set; }
        public DateTime TimeSent { get; set; }

        private List<Reaction> reactions = new List<Reaction>();
        public IReadOnlyCollection<Reaction> Reactions => reactions.AsReadOnly();
        public void AddReaction()
        {

        }
    }
}
