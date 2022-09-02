using Infrastructure.Identity;
using Infrastructure.StripePayments.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using WebServer.Modules.HostingInformation;
using WebShared.Misc.Attributes;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeTeamAdmin]
    public class StripeController : AuthorizedBaseController
    {
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IdentificationDbContext identificationDbContext;
        private readonly IStripeSessionService stripeSessionService;
        private readonly string returnUrl;
        public StripeController(ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, IStripeSessionService stripeSessionService, IServerInformationProvider serverInformationProvider)
        {
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.stripeSessionService = stripeSessionService;
            returnUrl = serverInformationProvider.BaseURI.AbsoluteUri;
        }

        public async Task<IActionResult> CancelSubscription()
        {
            return Ok();
        }

        [HttpPost("Subscribe/Premium")]
        public async Task<ActionResult> RedirectToStripePremiumSubscription()
        {
            var checkoutSession = stripeSessionService.CreateCheckoutSession(returnUrl, ApplicationUser.StripeCustomerId, Tenant.Id , null);         

            Response.Headers.Add("Location", checkoutSession.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost("Subscribe/Enterprise")]
        public async Task<ActionResult> RedirectToStripeEnterpriseSubscription()
        {
            var checkoutSession = stripeSessionService.CreateCheckoutSession(returnUrl, ApplicationUser.StripeCustomerId, Tenant.Id, null);

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
