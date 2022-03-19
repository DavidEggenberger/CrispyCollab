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
        public bool Votable { get; set; }

        private List<Vote> votes = new List<Vote>();
        public void AddVote(Vote vote)
        {
            if(votes.Any(v => v.CreatedByUserId == vote.CreatedByUserId) is false)
            {
                votes.Add(vote);
            }
        }
        public void RemoveVote(Vote vote)
        {
            votes.Remove(votes.FirstOrDefault(v => v.CreatedByUserId == vote.CreatedByUserId));
        }
        public List<Vote> MakeTopicVotes { get; set; }
    }
}
