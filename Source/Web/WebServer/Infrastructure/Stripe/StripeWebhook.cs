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

//namespace WebServer.Controllers.Identity
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StripeWebhook : ControllerBase
//    {
//        [HttpPost]
//        [IgnoreAntiforgeryToken]
//        [AllowAnonymous]
//        public async Task<IActionResult> Index()
//        {
//            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
//            const string endpointSecret = "";
//            try
//            {
//                var stripeEvent = EventUtility.ParseEvent(json);
//                var signatureHeader = Request.Headers["Stripe-Signature"];
//                stripeEvent = EventUtility.ConstructEvent(json,
//                        signatureHeader, endpointSecret);

//                if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
//                {
//                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
//                    if (team.Subscription is not null && team.Subscription.SubscriptionPlan.PlanType != SubscriptionPlanType.Free)
//                    {
//                        await subscriptionManager.CancelSubscriptionAsync(team.Subscription);
//                    }
//                    SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByStripePriceId(subscription.Items.First().Price.Id);
//                    team.Subscription = subscriptionManager.CreateSubscription(subscriptionPlan, subscription.CurrentPeriodEnd, subscription.Id);
//                    await adminNotificationManager.CreateNotification(team, AdminNotificationType.SubscriptionCreated, applicationUser, $"{applicationUser.UserName} created the {team.Subscription.SubscriptionPlan.PlanType} subscription");
//                    await identificationDbContext.SaveChangesAsync();
//                }
//                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
//                {
//                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
//                    ApplicationUser applicationUser = await applicationUserManager.FindUserByStripeCustomerId(subscription.CustomerId);
//                    Team team = await teamManager.FindByIdAsync(subscription.Metadata["TeamId"]);
//                    SubscriptionService subscriptionService = new SubscriptionService();
//                    SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByStripePriceId(subscription.Items.First().Price.Id);
//                    Infrastructure.Identity.Entities.Subscription _subscription = await subscriptionManager.FindSubscriptionByStripeSubscriptionId(subscription.Id);
//                    await identificationDbContext.SaveChangesAsync();
//                }
//                else if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
//                {
//                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
//                    ApplicationUser applicationUser = await applicationUserManager.FindUserByStripeCustomerId(subscription.CustomerId);
//                    Team team = await teamManager.FindByIdAsync(subscription.Metadata["TeamId"]);
//                    await adminNotificationManager.CreateNotification(team, AdminNotificationType.SubscriptionDeleted, applicationUser, $"{applicationUser.UserName} canceled the {team.Subscription.SubscriptionPlan.PlanType} subscription");
//                }
//                else if (stripeEvent.Type == Events.CustomerSubscriptionTrialWillEnd)
//                {
//                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
//                    ApplicationUser applicationUser = await applicationUserManager.FindUserByStripeCustomerId(subscription.CustomerId);
//                    Team team = await teamManager.FindByIdAsync(subscription.Metadata["TeamId"]);
//                    SubscriptionService subscriptionService = new SubscriptionService();
//                    SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByStripePriceId(subscription.Items.First().Price.Id);
//                }
//                return Ok();
//            }
//            catch (StripeException e)
//            {
//                return BadRequest();
//            }
//        }
//    }
//}
