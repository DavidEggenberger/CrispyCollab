using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subscription = Infrastructure.Identity.Entities.Subscription;

namespace Infrastructure.Identity.Services
{
    public class SubscriptionManager
    {
        private readonly IdentificationDbContext identificationDbContext;
        private readonly SubscriptionService subscriptionService;
        private readonly AdminNotificationManager adminNotificationManager;
        public SubscriptionManager(IdentificationDbContext identificationDbContext, AdminNotificationManager adminNotificationManager)
        {
            this.identificationDbContext = identificationDbContext;
            this.adminNotificationManager = adminNotificationManager;
            this.subscriptionService = new SubscriptionService();
        }
        public Subscription CreateSubscription(SubscriptionPlan subscriptionPlan, DateTime dateTime)
        {
            return new Subscription
            {
                SubscriptionPlan = subscriptionPlan,
                PeriodEnd = dateTime
            };
        }
        public Task<Subscription> FindSubscriptionByStripeSubscriptionId(string id)
        {
            try
            {
                return identificationDbContext.Subscriptions.SingleAsync(x => x.StripeSubscriptionId == id);
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException();
            }
        }
        public async Task SetSubscriptionForTeam(Subscription subscriptionPlan, Team team)
        {
            team.Subscription = subscriptionPlan;
            await identificationDbContext.SaveChangesAsync();
        }
        public Task<SubscriptionPlan> FindByIdAsync(Guid id)
        {
            return identificationDbContext.SubscriptionPlans.SingleAsync(x => x.Id == id);
        }
        public List<SubscriptionPlan> GetAllSubscriptionPlans()
        {
            return identificationDbContext.SubscriptionPlans.ToList();
        }
        public async Task CancelSubscriptionAsync(Subscription subscription)
        {
            var cancelOptions = new SubscriptionCancelOptions
            {
                InvoiceNow = false,
                Prorate = true
            };
            await subscriptionService.CancelAsync(subscription.StripeSubscriptionId, cancelOptions);
        }
    }
}
