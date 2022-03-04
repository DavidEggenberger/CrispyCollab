using Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class SubscriptionManager
    {
        private readonly IdentificationDbContext identificationDbContext;
        private readonly SubscriptionService subscriptionService;
        public SubscriptionManager(IdentificationDbContext identificationDbContext)
        {
            this.identificationDbContext = identificationDbContext;
            this.subscriptionService = new SubscriptionService();
        }
        public async Task<Entities.Subscription> CreateSubscription(SubscriptionPlan subscriptionPlan, DateTime dateTime)
        {
            return new Entities.Subscription
            {
                SubscriptionPlan = subscriptionPlan,
                PeriodEnd = dateTime
            };
        }
        public Task<Entities.Subscription> FindSubscriptionByStripeSubscriptionId(string id)
        {
            return identificationDbContext.Subscriptions.SingleOrDefaultAsync(x => x.StripeSubscriptionId == id);
        }
        public async Task SetSubscriptionForTeam(Entities.Subscription subscriptionPlan, Team team)
        {
            Team _team = await identificationDbContext.Teams.Include(t => t.Subscription).SingleOrDefaultAsync(t => t.Id == team.Id);
            _team.Subscription = subscriptionPlan;
            await identificationDbContext.SaveChangesAsync();
        }
    }
}
