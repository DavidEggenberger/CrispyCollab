using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private ApplicationUserManager applicationUserManager;
        private IdentificationDbContext identificationDbContext;
        public StripeController(ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext)
        {
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
        }
        [HttpPost("Subscribe")]
        public async Task<ActionResult> RedirectToSubscription()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            var domain = "https://localhost:44333";

            //var priceOptions = new PriceListOptions
            //{
            //    LookupKeys = new List<string> {
            //        Request.Form["lookup_key"]
            //    }
            //};
            //var priceService = new PriceService();
            //StripeList<Price> prices = priceService.List(priceOptions);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                Customer = applicationUser.StripeCustomerId,
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                        Price = "price_1JvrJXEhLcfJYVVFHgWGpFlD",
                        Quantity = 1,
                  },
                },
                Mode = "subscription",
                SuccessUrl = domain + "/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/cancel",
                SubscriptionData = new SessionSubscriptionDataOptions
                {
                    TrialPeriodDays = 31
                }
            };
            var service = new SessionService();
            Session session = null;
            try
            {
                session = service.Create(options);
            }
            catch(Exception ex)
            {

            }

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [IgnoreAntiforgeryToken]
        [HttpPost("webhooks")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            // Replace this endpoint secret with your endpoint's unique secret
            // If you are testing with the CLI, find the secret by running 'stripe listen'
            // If you are using an endpoint defined with the API or dashboard, look in your webhook settings
            // at https://dashboard.stripe.com/webhooks
            const string endpointSecret = "whsec_1e2d0609f798c0d2b32de188b680fa5edafbd3212dead57d24b0e408085f8bd4";
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);

                if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    var result = await applicationUserManager.FindByStripeCustomerId(subscription.CustomerId);
                    if(result.Successful is false)
                    {
                        throw new Exception();
                    }
                    ApplicationUser applicationUser = result.Value;

                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    var result = await applicationUserManager.FindByStripeCustomerId(subscription.CustomerId);
                    if (result.Successful is false)
                    {
                        throw new Exception();
                    }
                    ApplicationUser applicationUser = result.Value;
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    var result = await applicationUserManager.FindByStripeCustomerId(subscription.CustomerId);
                    if (result.Successful is false)
                    {
                        throw new Exception();
                    }
                    ApplicationUser applicationUser = result.Value;
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionTrialWillEnd)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    var result = await applicationUserManager.FindByStripeCustomerId(subscription.CustomerId);
                    if (result.Successful is false)
                    {
                        throw new Exception();
                    }
                    ApplicationUser applicationUser = result.Value;
                }
                else
                {
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
        [Route("create-portal-session")]
        [ApiController]
        public class PortalApiController : Controller
        {
            [HttpPost]
            public ActionResult Create()
            {
                // For demonstration purposes, we're using the Checkout session to retrieve the customer ID.
                // Typically this is stored alongside the authenticated user in your database.
                var checkoutService = new SessionService();
                var checkoutSession = checkoutService.Get(Request.Form["session_id"]);

                // This is the URL to which your customer will return after
                // they are done managing billing in the Customer Portal.
                var returnUrl = "https://localhost:44333";

                var options = new Stripe.BillingPortal.SessionCreateOptions
                {
                    Customer = checkoutSession.CustomerId,
                    ReturnUrl = returnUrl,
                };
                var service = new Stripe.BillingPortal.SessionService();
                var session = service.Create(options);

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
        }
    }
}
