﻿using Shared.Features.Domain.Exceptions;

namespace Modules.Channels.Features.DomainFeatures.Channels.Exceptions
{
    public class InvalidVoteCastException : DomainException
    {
        public InvalidVoteCastException(string userId, string voteId) : base($"User with Id {userId} voted for {voteId} already")
        {
        }
    }
}
