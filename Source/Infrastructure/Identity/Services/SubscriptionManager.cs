using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types;
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
        public Entities.Subscription CreateSubscription(SubscriptionPlan subscriptionPlan, DateTime dateTime)
        {
            return new Entities.Subscription
            {
                SubscriptionPlan = subscriptionPlan,
                PeriodEnd = dateTime
            };
        }
        public Task<Entities.Subscription> FindSubscriptionByStripeSubscriptionId(string id)
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
        public async Task SetSubscriptionForTeam(Entities.Subscription subscriptionPlan, Team team)
        {
            Team _team = await identificationDbContext.Teams.Include(t => t.Subscription).SingleOrDefaultAsync(t => t.Id == team.Id);
            _team.Subscription = subscriptionPlan;
            await identificationDbContext.SaveChangesAsync();
        }
    }
}
