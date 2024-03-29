using Shared.Features.Domain.Exceptions;
using Shared.Features.DomainKernel;

namespace Modules.Channels.Features.DomainFeatures.ChannelAggregate.Exceptions
{
    public class InvalidVoteCastException : DomainException
    {
        public InvalidVoteCastException(string userId, string voteId) : base($"User with Id {userId} voted for {voteId} already")
        {
        }
    }
}
