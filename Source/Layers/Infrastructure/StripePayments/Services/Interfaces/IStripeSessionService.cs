using Infrastructure.StripePayments.Models;

namespace Infrastructure.StripePayments.Services.Interfaces
{
    public interface IStripeSessionService
    {
        Stripe.BillingPortal.Session CreateBillingPortalSession(string redirectBaseUrl, string stripeCustomerId);
        Stripe.Checkout.Session CreateCheckoutSession(string redirectBaseUrl, string stripeCustomerId, Guid tenantId, StripeSubscription stripeSubscription);
    }
}
