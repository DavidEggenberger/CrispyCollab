using Infrastructure.Identity;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

//namespace WebServer.Controllers.Identity
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [AuthorizeTeamAdmin]
//    public class StripeController : ControllerBase
//    {
//        //private readonly AdminNotificationManager adminNotificationManager;
//        private readonly ApplicationUserManager applicationUserManager;
//        private readonly IdentificationDbContext identificationDbContext;
//        //private readonly SubscriptionPlanManager subscriptionPlanManager;
//        //private readonly SubscriptionManager subscriptionManager;
//        //private readonly TeamManager teamManager;
//        public StripeController(ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SubscriptionPlanManager subscriptionPlanManager, TeamManager teamManager, SubscriptionManager subscriptionManager, AdminNotificationManager adminNotificationManager)
//        {
//            this.applicationUserManager = applicationUserManager;
//            this.identificationDbContext = identificationDbContext;
//            this.subscriptionPlanManager = subscriptionPlanManager;
//            this.teamManager = teamManager;
//            this.subscriptionManager = subscriptionManager;
//            this.adminNotificationManager = adminNotificationManager;
//        }

//        public async Task<IActionResult> CancelSubscription()
//        {
//            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
//            await subscriptionManager.CancelSubscriptionAsync(applicationUser.SelectedTeam.Subscription);
//            return Ok();
//        }
        
//        [HttpPost("Subscribe/Premium")]
//        public async Task<ActionResult> RedirectToStripePremiumSubscription()
//        {
//            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
//            Team selectedTeam = applicationUser.SelectedTeam;
//            SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByPlanType(SubscriptionPlanType.Premium);
//            if(selectedTeam.Subscription.SubscriptionPlan.PlanType == SubscriptionPlanType.Premium)
//            {
//                return LocalRedirect("/ManageTeam");
//            }
//            var domain = "https://localhost:44333";

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
//            Team selectedTeam = applicationUser.SelectedTeam;
//            SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByPlanType(SubscriptionPlanType.Enterprise);
//            if (selectedTeam.Subscription.SubscriptionPlan.PlanType == SubscriptionPlanType.Enterprise)
//            {
//                return LocalRedirect("/ManageTeam");
//            }
//            var domain = "https://localhost:44333";

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

//        [HttpPost("webhooks")]
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
//                    ApplicationUser applicationUser = await applicationUserManager.FindUserByStripeCustomerId(subscription.CustomerId);
//                    Team team = await teamManager.FindByIdAsync(subscription.Metadata["TeamId"]);
//                    if(team.Subscription is not null && team.Subscription.SubscriptionPlan.PlanType != SubscriptionPlanType.Free)
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
