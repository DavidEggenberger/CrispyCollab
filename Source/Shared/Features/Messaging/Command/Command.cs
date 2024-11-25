namespace Shared.Features.Messaging.Command
{
    public class Command
    {
        public Guid ExecutingUserId { get; set; }
    }

    public class ICommand<IResponse>
    {
        public Guid ExecutingUserId { get; set; }
    }
}
