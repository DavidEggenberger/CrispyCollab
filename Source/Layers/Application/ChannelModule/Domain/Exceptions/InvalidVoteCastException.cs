using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ChannelAggregate.Exceptions
{
    public class InvalidVoteCastException : DomainException
    {
        public InvalidVoteCastException(string userId, string voteId) : base($"User with Id {userId} voted for {voteId} already")
        {
        }
    }
}
