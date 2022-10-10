using Infrastructure.Identity;
using Infrastructure.StripeIntegration.Configuration;

namespace Infrastructure.StripeIntegration.Services.Interfaces
{
    public interface IStripeSessionService
    {
        Task<Stripe.Checkout.Session> GetStripeCheckoutSession(string id);
        Task<Stripe.BillingPortal.Session> CreateBillingPortalSession(string redirectBaseUrl, string stripeCustomerId);
        Task<Stripe.Checkout.Session> CreateCheckoutSession(string redirectBaseUrl, ApplicationUser user, Guid tenantId, StripeSubscriptionType stripeSubscription);
    }
}
