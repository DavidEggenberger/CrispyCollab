namespace SubscriptionModule.Server.Features.Commands
{
    public class SubscriptionTrialEndedCommand : ICommand
    {
        public Subscription Subscription { get; set; }
    }
}
