using Common.Misc.Attributes;
using Infrastructure.Identity;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Types.Enums;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IdentificationDbContext identificationDbContext;
        private readonly SubscriptionPlanManager subscriptionPlanManager;
        private readonly SubscriptionManager subscriptionManager;
        private readonly TeamManager teamManager;
        public StripeController(ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SubscriptionPlanManager subscriptionPlanManager, TeamManager teamManager, SubscriptionManager subscriptionManager)
        {
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.subscriptionPlanManager = subscriptionPlanManager;
            this.teamManager = teamManager;
            this.subscriptionManager = subscriptionManager;
        }

        [AuthorizeTeamAdmin]
        public async Task<IActionResult> CancelSubscription()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            Team selectedTeam = (await teamManager.GetCurrentSelectedTeamForApplicationUserAsync(applicationUser)).Value;
            identificationDbContext.Entry(selectedTeam).Reference(s => s.Subscription).Load();
            identificationDbContext.Entry(selectedTeam.Subscription).Reference(s => s.SubscriptionPlan).Load();
            var service = new SubscriptionService();
            var cancelOptions = new SubscriptionCancelOptions
            {
                InvoiceNow = false,
                Prorate = false,
            };
            Stripe.Subscription subscription = await service.CancelAsync(selectedTeam.Subscription.StripeSubscriptionId, cancelOptions);
            return Ok();
        }

        [HttpPost("Subscribe/Premium")]
        [AuthorizeTeamAdmin]
        public async Task<ActionResult> RedirectToStripePremiumSubscription()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            Team selectedTeam = (await teamManager.GetCurrentSelectedTeamForApplicationUserAsync(applicationUser)).Value;
            SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByPlanType(SubscriptionPlanType.Premium);
            identificationDbContext.Entry(selectedTeam).Reference(x => x.Subscription).Load();
            identificationDbContext.Entry(selectedTeam.Subscription).Reference(x => x.SubscriptionPlan).Load();
            if(selectedTeam.Subscription.SubscriptionPlan.PlanType == SubscriptionPlanType.Premium)
            {
                return LocalRedirect("/Identity/TeamManagement/SubscriptionPlan");
            }
            var domain = "https://localhost:44333";

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
                        Price = subscriptionPlan.StripePriceId,
                        Quantity = 1
                    }
                },
                Mode = "subscription",
                SuccessUrl = domain + "/Identity/Stripe/Success",
                CancelUrl = domain + "/Identity/Stripe/Cancel",
                SubscriptionData = new SessionSubscriptionDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        ["TeamId"] = selectedTeam.Id.ToString()
                    },
                    TrialPeriodDays = subscriptionPlan.TrialPeriodDays == 0 ? 7 : subscriptionPlan.TrialPeriodDays
                }
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost("Subscribe/Enterprise")]
        [AuthorizeTeamAdmin]
        public async Task<ActionResult> RedirectToStripeEnterpriseSubscription()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            Team selectedTeam = (await teamManager.GetCurrentSelectedTeamForApplicationUserAsync(applicationUser)).Value;
            SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByPlanType(SubscriptionPlanType.Enterprise);
            identificationDbContext.Entry(selectedTeam).Reference(x => x.Subscription).Load();
            identificationDbContext.Entry(selectedTeam.Subscription).Reference(x => x.SubscriptionPlan).Load();
            if (selectedTeam.Subscription.SubscriptionPlan.PlanType == SubscriptionPlanType.Enterprise)
            {
                return LocalRedirect("/Identity/TeamManagement/SubscriptionPlan");
            }
            var domain = "https://localhost:44333";

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
                        Price = subscriptionPlan.StripePriceId,
                        Quantity = 1
                    }
                },
                Mode = "subscription",
                SuccessUrl = domain + "/Identity/Stripe/Success",
                CancelUrl = domain + "/Identity/Stripe/Cancel",
                SubscriptionData = new SessionSubscriptionDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        ["TeamId"] = selectedTeam.Id.ToString()
                    },
                    TrialPeriodDays = subscriptionPlan.TrialPeriodDays == 0 ? 7 : subscriptionPlan.TrialPeriodDays
                }
            };
            var service = new SessionService();
            Session session = service.Create(options);
            
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [IgnoreAntiforgeryToken]
        [HttpPost("webhooks")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_1e2d0609f798c0d2b32de188b680fa5edafbd3212dead57d24b0e408085f8bd4";
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);

                if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    ApplicationUser applicationUser = await applicationUserManager.FindByStripeCustomerId(subscription.CustomerId);
                    Team team = await teamManager.FindByIdAsync(subscription.Metadata["TeamId"]);
                    SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByStripePriceId(subscription.Items.First().Price.Id);
                    team.Subscription = await subscriptionManager.CreateSubscription(subscriptionPlan, subscription.CurrentPeriodEnd);
                    team.Subscription.StripeSubscriptionId = subscription.Id;
                    await identificationDbContext.SaveChangesAsync();
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    ApplicationUser applicationUser = await applicationUserManager.FindByStripeCustomerId(subscription.CustomerId);
                    Team team = await teamManager.FindByIdAsync(subscription.Metadata["TeamId"]);
                    SubscriptionService subscriptionService = new SubscriptionService();
                    SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByStripePriceId(subscription.Items.First().Price.Id);
                    Infrastructure.Identity.Entities.Subscription _subscription = await subscriptionManager.FindSubscriptionByStripeSubscriptionId(subscription.Id);
                    //_subscription.Status = (SubscriptionStatus)subscription.Status;
                    await identificationDbContext.SaveChangesAsync();
                }  
                else if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;

                    ApplicationUser applicationUser = await applicationUserManager.FindByStripeCustomerId(subscription.CustomerId);
                    Team team = await teamManager.FindByIdAsync(subscription.Metadata["TeamId"]);
                    SubscriptionService subscriptionService = new SubscriptionService();
                    SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByPlanType(SubscriptionPlanType.Free);
                    team.Subscription = await subscriptionManager.CreateSubscription(subscriptionPlan, subscription.CurrentPeriodEnd);
                    await identificationDbContext.SaveChangesAsync();
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionTrialWillEnd)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    ApplicationUser applicationUser = await applicationUserManager.FindByStripeCustomerId(subscription.CustomerId);
                    Team team = await teamManager.FindByIdAsync(subscription.Metadata["TeamId"]);
                    SubscriptionService subscriptionService = new SubscriptionService();
                    SubscriptionPlan subscriptionPlan = await subscriptionPlanManager.FindByStripePriceId(subscription.Items.First().Price.Id);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

        [IgnoreAntiforgeryToken]
        [Route("create-portal-session")]
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);

            var returnUrl = "https://localhost:44333";

            var options = new Stripe.BillingPortal.SessionCreateOptions
            {
                Customer = applicationUser.StripeCustomerId,
                ReturnUrl = returnUrl,
            };
            var service = new Stripe.BillingPortal.SessionService();
            var session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
