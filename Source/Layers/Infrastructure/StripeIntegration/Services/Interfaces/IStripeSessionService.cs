using Infrastructure.Identity;
using Infrastructure.StripeIntegration.Configuration;

namespace Infrastructure.StripeIntegration.Services.Interfaces
{
    public interface IStripeSessionService
    {
        Stripe.BillingPortal.Session CreateBillingPortalSession(string redirectBaseUrl, string stripeCustomerId);
        Stripe.Checkout.Session CreateCheckoutSession(string redirectBaseUrl, ApplicationUser user, Guid tenantId, StripeSubscriptionType stripeSubscription);
    }
}
