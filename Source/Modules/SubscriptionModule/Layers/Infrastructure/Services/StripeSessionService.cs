using Infrastructure.Identity;
using Infrastructure.StripeIntegration.Services.Interfaces;
using Stripe.Checkout;
using Infrastructure.StripeIntegration.Configuration;

namespace Infrastructure.StripeIntegration.Services
{
    public class StripeSessionService : IStripeSessionService
    {
        public async Task<Stripe.BillingPortal.Session> CreateBillingPortalSessionAsync(string redirectBaseUrl, string stripeCustomerId)
        {
            var options = new Stripe.BillingPortal.SessionCreateOptions
            {
                Customer = stripeCustomerId,
                ReturnUrl = redirectBaseUrl
            };
            var service = new Stripe.BillingPortal.SessionService();
            Stripe.BillingPortal.Session session = await service.CreateAsync(options);
            return session;
        }

        public async Task<Stripe.Checkout.Session> CreateCheckoutSessionAsync(string redirectBaseUrl, ApplicationUser user, Guid tenantId, StripeSubscriptionType stripeSubscription)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                Customer = user.StripeCustomerId,
                CustomerEmail = user.Email,
                ClientReferenceId = user.Id.ToString(),
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = stripeSubscription.StripePriceId,
                        Quantity = 1
                    }
                },
                Mode = "subscription",
                SuccessUrl = redirectBaseUrl + "/ManageTeam",
                CancelUrl = redirectBaseUrl + "/ManageTeam",
                SubscriptionData = new SessionSubscriptionDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        ["TenantId"] = tenantId.ToString(),
                    },
                    TrialPeriodDays = stripeSubscription.TrialPeriodDays
                }
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session;
        }

        public async Task<Session> GetStripeCheckoutSessionAsync(string id)
        {
            var session = await new SessionService().GetAsync(id);
            return session;
        }
    }
}
