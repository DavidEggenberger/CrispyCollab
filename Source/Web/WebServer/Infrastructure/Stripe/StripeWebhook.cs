using Infrastructure.StripePayments.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using System.IO;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebhook : ControllerBase
    {
        private readonly StripeOptions stripeOptions;
        public StripeWebhook(IOptions<StripeOptions> stripeOptions)
        {
            this.stripeOptions = stripeOptions.Value;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, stripeOptions.EndpointSecret);

                if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionTrialWillEnd)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    
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
