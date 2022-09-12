using Infrastructure.Identity.Entities;
using Infrastructure.Identity;
using Infrastructure.StripeIntegration.Services.Interfaces;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.StripeIntegration.Configuration;

namespace Infrastructure.StripeIntegration.Services
{
    public class StripeSessionService : IStripeSessionService
    {
        public Stripe.BillingPortal.Session CreateBillingPortalSession(string redirectBaseUrl, string stripeCustomerId)
        {
            var options = new Stripe.BillingPortal.SessionCreateOptions
            {
                Customer = stripeCustomerId,
                ReturnUrl = redirectBaseUrl,
            };
            var service = new Stripe.BillingPortal.SessionService();
            return service.Create(options);
        }

        public Stripe.Checkout.Session CreateCheckoutSession(string redirectBaseUrl, ApplicationUser user, Guid tenantId, StripeSubscriptionType stripeSubscription)
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
            Session session = service.Create(options);
            return session;
        }
    }
}
