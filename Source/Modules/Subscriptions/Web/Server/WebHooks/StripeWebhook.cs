using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Shared.Features.Server;
using Modules.Subscriptions.Features;

namespace Web.Server.Controllers.Stripe
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebhook : BaseController<SubscriptionsModule>
    {

        public StripeWebhook(IServiceProvider serviceProvider) : base(serviceProvider) { }

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
                        signatureHeader, module.SubscriptionsConfiguration.StripeEndpointSecret);

                //More Events Events.Checkout...
                if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    await commandDispatcher.DispatchAsync(new CreateSubscription { Subscription = subscription });
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    await commandDispatcher.DispatchAsync(new UpdateSubscription { Subscription = subscription });
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    await commandDispatcher.DispatchAsync(new DeleteSubscription { Subscription = subscription });
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionTrialWillEnd)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    await commandDispatcher.DispatchAsync(new SubscriptionTrialEnded { Subscription = subscription });
                }
                return Ok();
            }
            catch (StripeException e)
            {
                throw new StripeIntegrationException(e.Message);
            }
        }
    }
}
