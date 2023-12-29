namespace SubscriptionModule.Server.Features.Commands
{
    public class DeleteSubscriptionCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
