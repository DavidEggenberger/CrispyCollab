using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.TopicAggregate
{
    [AggregateRoot]
    public class Topic : Entity
    {
        public string Name { get; set; }
        public string Goal { get; set; }

        private List<Approach> approaches = new List<Approach>();
        public IReadOnlyCollection<Approach> Approaches => approaches.AsReadOnly();
        private Topic() { }
        public Topic(string name, string goal)
        {
            Name = name;
            Goal = goal;
        }
        public void AddApproach(Approach approach)
        {
            approaches.Add(approach);
        }
        public void RemoveApproach(Approach approach)
        {

        }
    }
}
