﻿using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types;
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
        public Task<SubscriptionPlan> FindById(Guid id)
        {
            return identificationDbContext.SubscriptionPlans.FindAsync(id).AsTask();
        }
        public Task<SubscriptionPlan> FindByStripePriceId(string id)
        {
            SubscriptionPlan subscriptionPlan;
            try
            {
                return identificationDbContext.SubscriptionPlans.SingleAsync(x => x.StripePriceId == id);
            }
            catch(Exception ex)
            {
                throw new IdentityOperationException("");
            }
        }
        public Task<SubscriptionPlan> FindByPlanType(SubscriptionPlanType planType)
        {
            try
            {
                return identificationDbContext.SubscriptionPlans.SingleAsync(sp => sp.PlanType == planType);
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException();
            }
        }
        public Task<List<SubscriptionPlan>> LoadAllSubscriptionPlans()
        {
            return identificationDbContext.SubscriptionPlans.OrderBy(x => x.PlanType).ToListAsync();
        }
        public Task<List<SubscriptionPlan>> LoadAllSubscriptionOlansExcept(SubscriptionPlan subscriptionPlan)
        {
            return identificationDbContext.SubscriptionPlans.Where(x => x.Name != subscriptionPlan.Name).OrderBy(x => x.PlanType).ToListAsync();
        }
    }
}