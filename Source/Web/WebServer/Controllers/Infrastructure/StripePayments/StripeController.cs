using Infrastructure.Identity;
using Infrastructure.StripePayments.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebServer.Modules.HostingInformation;
using WebShared.Misc.Attributes;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeTeamAdmin]
    public class StripeController : AuthorizedBaseController
    {
        private readonly IStripeSessionService stripeSessionService;
        private readonly IStripeSubscriptionService stripeSubscriptionService;
        private readonly string returnUrl;
        public StripeController(IStripeSessionService stripeSessionService, IServerInformationProvider serverInformationProvider, IStripeSubscriptionService stripeSubscriptionService)
        {
            this.stripeSessionService = stripeSessionService;
            this.stripeSubscriptionService = stripeSubscriptionService;
            returnUrl = serverInformationProvider.BaseURI.AbsoluteUri;
        }

        public async Task<IActionResult> CancelSubscription()
        {
            return Ok();
        }

        [HttpPost("Subscribe/Premium")]
        public async Task<ActionResult> RedirectToStripePremiumSubscription()
        {
            var premiumSubscription = stripeSubscriptionService.GetSubscriptionFromPlanType(SubscriptionPlanType.Premium);
            var checkoutSession = stripeSessionService.CreateCheckoutSession(returnUrl, ApplicationUser.StripeCustomerId, Tenant.Id , premiumSubscription);         

            Response.Headers.Add("Location", checkoutSession.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost("Subscribe/Enterprise")]
        public async Task<ActionResult> RedirectToStripeEnterpriseSubscription()
        {
            var enterpriseSubscription = stripeSubscriptionService.GetSubscriptionFromPlanType(SubscriptionPlanType.Enterprise);
            var checkoutSession = stripeSessionService.CreateCheckoutSession(returnUrl, ApplicationUser.StripeCustomerId, Tenant.Id, enterpriseSubscription);

            Response.Headers.Add("Location", checkoutSession.Url);
            return new StatusCodeResult(303);
        }

        [Route("create-portal-session")]
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var billingPortalSession = stripeSessionService.CreateBillingPortalSession(returnUrl, ApplicationUser.StripeCustomerId);

            Response.Headers.Add("Location", billingPortalSession.Url);
            return new StatusCodeResult(303);
        }
    }
}
