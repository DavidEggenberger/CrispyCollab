namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate
{
    public enum SubscriptionStatus
    {
        Active,
        Canceled,
        Trialing,
        Unpaid
    }
}
