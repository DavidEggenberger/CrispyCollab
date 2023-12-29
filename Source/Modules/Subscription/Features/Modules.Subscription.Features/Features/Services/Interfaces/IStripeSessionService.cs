using SubscriptionModule.Server.Features.Configuration;

namespace SubscriptionModule.Server.Features.Services.Interfaces
{
    public interface IStripeSessionService
    {
        Task<Stripe.Checkout.Session> GetStripeCheckoutSessionAsync(string id);
        Task<Stripe.BillingPortal.Session> CreateBillingPortalSessionAsync(string redirectBaseUrl, string stripeCustomerId);
        Task<Stripe.Checkout.Session> CreateCheckoutSessionAsync(string redirectBaseUrl, string stripeCustomerId, Guid tenantId, StripeSubscriptionType stripeSubscription);
    }
}
