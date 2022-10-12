//using Infrastructure.CQRS.Command;
//using Infrastructure.StripeIntegration;
//using Infrastructure.StripeIntegration.Commands;
//using Infrastructure.StripeIntegration.Configuration;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using Stripe;
//using System.IO;

//namespace WebServer.Controllers.Stripe
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StripeWebhook : ControllerBase
//    {
//        private readonly StripeOptions stripeOptions;
//        private readonly ICommandDispatcher commandDispatcher;
//        public StripeWebhook(IOptions<StripeOptions> stripeOptions, ICommandDispatcher commandDispatcher)
//        {
//            this.stripeOptions = stripeOptions.Value;
//            this.commandDispatcher = commandDispatcher;
//        }

//        [HttpPost]
//        [IgnoreAntiforgeryToken]
//        [AllowAnonymous]
//        public async Task<IActionResult> Index()
//        {
//            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
//            try
//            {
//                var stripeEvent = EventUtility.ParseEvent(json);
//                var signatureHeader = Request.Headers["Stripe-Signature"];
//                stripeEvent = EventUtility.ConstructEvent(json,
//                        signatureHeader, stripeOptions.EndpointSecret);

//                //More Events Events.Checkout...
//                if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
//                {
//                    var subscription = stripeEvent.Data.Object as Subscription;
//                    await commandDispatcher.DispatchAsync(new CreateSubscriptionCommand { Subscription = subscription });
//                }
//                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
//                {
//                    var subscription = stripeEvent.Data.Object as Subscription;
//                    await commandDispatcher.DispatchAsync(new UpdateSubscriptionCommand { Subscription = subscription });
//                }
//                else if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
//                {
//                    var subscription = stripeEvent.Data.Object as Subscription;
//                    await commandDispatcher.DispatchAsync(new DeleteSubscriptionCommand { Subscription = subscription });
//                }
//                else if (stripeEvent.Type == Events.CustomerSubscriptionTrialWillEnd)
//                {
//                    var subscription = stripeEvent.Data.Object as Subscription;
//                    await commandDispatcher.DispatchAsync(new SubscriptionTrialEndedCommand { Subscription = subscription });
//                }
//                return Ok();
//            }
//            catch (StripeException e)
//            {
//                throw new StripeIntegrationException(e.Message);
//            }
//        }
//    }
//}
