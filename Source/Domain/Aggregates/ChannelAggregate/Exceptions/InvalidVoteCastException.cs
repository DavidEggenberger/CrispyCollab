using Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.ChannelAggregate.Exceptions
{
    public class InvalidVoteCastException : DomainException
    {
        public InvalidVoteCastException(string userId) : base($"User with Id {userId} voted already")
        {
        }
    }
}
