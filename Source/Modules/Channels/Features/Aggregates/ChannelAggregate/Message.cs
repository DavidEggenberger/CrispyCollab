﻿using Modules.Channels.Features.Aggregates.ChannelAggregate.Events;
using Shared.Features.DomainKernel;

namespace Modules.Channels.Features.Aggregates.ChannelAggregate
{
    public class Message : Entity
    {
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
        public DateTime TimeSent { get; set; }
        public string Text { get; set; }
        public bool HasDerivedTopic { get; set; }
        public string DerivedTopicId { get; set; }
        public bool Votable { get; set; }

        private List<Reaction> votes = new List<Reaction>();
        public List<Reaction> MakeMessageTopicVotes => votes;
        internal void AddVote(Reaction vote)
        {
            if (votes.Any(v => v.CreatedByUserId == vote.CreatedByUserId) is false)
            {
                votes.Add(vote);
                AddDomainEvent(new MessageVotesUpdatedEvent());
            }
        }
        internal void RemoveVote(Reaction vote)
        {
            votes.Remove(votes.FirstOrDefault(v => v.CreatedByUserId == vote.CreatedByUserId));
            AddDomainEvent(new MessageVotesUpdatedEvent());
        }
    }
}
