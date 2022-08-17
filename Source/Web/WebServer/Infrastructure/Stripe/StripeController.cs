//using Infrastructure.Identity;
//using Infrastructure.Identity.Entities;
//using Infrastructure.Identity.Types.Enums;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Stripe;
//using Stripe.Checkout;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using WebServer.Modules.HostingInformation;
//using WebShared.Misc.Attributes;

//namespace WebServer.Controllers.Identity
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [AuthorizeTeamAdmin]
//    public class StripeController : ControllerBase
//    {
//        private readonly ApplicationUserManager applicationUserManager;
//        private readonly IdentificationDbContext identificationDbContext;
//        private readonly string domain;
//        public StripeController(ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, IServerInformationProvider serverInformationProvider)
//        {
//            this.applicationUserManager = applicationUserManager;
//            this.identificationDbContext = identificationDbContext;
//            domain = serverInformationProvider.BaseURI.ToString();
//        }

//        public async Task<IActionResult> CancelSubscription()
//        {
//            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
//            return Ok();
//        }

//        [HttpPost("Subscribe/Premium")]
//        public async Task<ActionResult> RedirectToStripePremiumSubscription()
//        {
//            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);

//            var options = new SessionCreateOptions
//            {
//                PaymentMethodTypes = new List<string>
//                {
//                  "card",
//                },
//                Customer = applicationUser.StripeCustomerId,
//                LineItems = new List<SessionLineItemOptions>
//                {
//                    new SessionLineItemOptions
//                    {
//                        Price = subscriptionPlan.StripePriceId,
//                        Quantity = 1
//                    }
//                },
//                Mode = "subscription",
//                SuccessUrl = domain + "/ManageTeam",
//                CancelUrl = domain + "/ManageTeam",
//                SubscriptionData = new SessionSubscriptionDataOptions
//                {
//                    Metadata = new Dictionary<string, string>
//                    {
//                        ["TeamId"] = selectedTeam.Id.ToString()
//                    },
//                    TrialPeriodDays = subscriptionPlan.TrialPeriodDays == 0 ? 7 : subscriptionPlan.TrialPeriodDays
//                }
//            };
//            var service = new SessionService();
//            Session session = service.Create(options);

//            Response.Headers.Add("Location", session.Url);
//            return new StatusCodeResult(303);
//        }

//        [HttpPost("Subscribe/Enterprise")]
//        public async Task<ActionResult> RedirectToStripeEnterpriseSubscription()
//        {
//            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);

//            var options = new SessionCreateOptions
//            {
//                PaymentMethodTypes = new List<string>
//                {
//                    "card",
//                },
//                Customer = applicationUser.StripeCustomerId,
//                LineItems = new List<SessionLineItemOptions>
//                {
//                    new SessionLineItemOptions
//                    {
//                        Price = subscriptionPlan.StripePriceId,
//                        Quantity = 1
//                    }
//                },
//                Mode = "subscription",
//                SuccessUrl = domain + "/ManageTeam",
//                CancelUrl = domain + "/ManageTeam",
//                SubscriptionData = new SessionSubscriptionDataOptions
//                {
//                    Metadata = new Dictionary<string, string>
//                    {
//                        ["TeamId"] = selectedTeam.Id.ToString()
//                    },
//                    TrialPeriodDays = subscriptionPlan.TrialPeriodDays == 0 ? 7 : subscriptionPlan.TrialPeriodDays
//                }
//            };
//            var service = new SessionService();
//            Session session = service.Create(options);

//            Response.Headers.Add("Location", session.Url);
//            return new StatusCodeResult(303);
//        }

//        [Route("create-portal-session")]
//        [HttpPost]
//        public async Task<ActionResult> Create()
//        {
//            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);

//            var returnUrl = "https://localhost:44333";

//            var options = new Stripe.BillingPortal.SessionCreateOptions
//            {
//                Customer = applicationUser.StripeCustomerId,
//                ReturnUrl = returnUrl,
//            };
//            var service = new Stripe.BillingPortal.SessionService();
//            var session = service.Create(options);

//            Response.Headers.Add("Location", session.Url);
//            return new StatusCodeResult(303);
//        }
//    }
//}
