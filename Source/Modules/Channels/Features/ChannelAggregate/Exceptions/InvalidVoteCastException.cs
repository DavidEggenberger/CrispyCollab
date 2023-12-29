namespace Modules.Channels.Features.ChannelAggregate.Exceptions
{
    public class InvalidVoteCastException : DomainException
    {
        public InvalidVoteCastException(string userId, string voteId) : base($"User with Id {userId} voted for {voteId} already")
        {
        }
    }
}
