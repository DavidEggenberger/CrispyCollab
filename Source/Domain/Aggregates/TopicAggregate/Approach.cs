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

        private List<Reaction> reactions = new List<Reaction>();
        public IReadOnlyCollection<Reaction> Reactions => reactions.AsReadOnly();
        private Approach() { }
        public Approach(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public void AddReaction()
        {

        }
    }
}
