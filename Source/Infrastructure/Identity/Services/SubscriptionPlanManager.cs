using Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class SubscriptionPlanManager
    {
        private readonly IdentificationDbContext identificationDbContext;
        private readonly SubscriptionService subscriptionService;
        public SubscriptionPlanManager(IdentificationDbContext identificationDbContext)
        {
            this.identificationDbContext = identificationDbContext;
            this.subscriptionService = new SubscriptionService();
        }
        public async Task CreateSubscription(SubscriptionPlan subscriptionPlan)
        {
            identificationDbContext.SubscriptionPlans.Add(subscriptionPlan);
            await identificationDbContext.SaveChangesAsync();
        }
        public Task<SubscriptionPlan> FindById(Guid id)
        {
            return identificationDbContext.SubscriptionPlans.FindAsync(id).AsTask();
        }
        public Task<SubscriptionPlan> FindByStripeSubscriptionId(string id)
        {
            return identificationDbContext.SubscriptionPlans.SingleAsync(x => x.StripeSubscriptionId == id);
        }
        public Task<SubscriptionPlan> FindByPlanType(PlanType planType)
        {
            return identificationDbContext.SubscriptionPlans.SingleAsync(sp => sp.PlanType == planType);
        }
        public Task<List<SubscriptionPlan>> LoadAllSubscriptionPlans()
        {
            return identificationDbContext.SubscriptionPlans.OrderBy(x => x.PlanType).ToListAsync();
        }
        public Task<List<SubscriptionPlan>> LoadAllSubscriptionExceptPlans(SubscriptionPlan subscriptionPlan)
        {
            return identificationDbContext.SubscriptionPlans.Where(x => x.Name != subscriptionPlan.Name).OrderBy(x => x.PlanType).ToListAsync();
        }
    }
}
