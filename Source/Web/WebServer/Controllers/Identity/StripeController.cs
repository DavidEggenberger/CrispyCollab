using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        [HttpPost("Subscribe")]
        public ActionResult RedirectToSubscription()
        {
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
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        [HttpPost("webhooks")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            // Replace this endpoint secret with your endpoint's unique secret
            // If you are testing with the CLI, find the secret by running 'stripe listen'
            // If you are using an endpoint defined with the API or dashboard, look in your webhook settings
            // at https://dashboard.stripe.com/webhooks
            const string endpointSecret = "whsec_0LJrek0NHVICP2d5fm0BtsZmTzMtVacJ";
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);
                if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    // Then define and call a method to handle the successful payment intent.
                    // handleSubscriptionCanceled(subscription);
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    // Then define and call a method to handle the successful payment intent.
                    // handleSubscriptionUpdated(subscription);
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    // Then define and call a method to handle the successful payment intent.
                    // handleSubscriptionUpdated(subscription);
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionTrialWillEnd)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    // Then define and call a method to handle the successful payment intent.
                    // handleSubscriptionUpdated(subscription);
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
    }
}
