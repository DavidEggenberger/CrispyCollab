using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Identity.Entities;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Identity
{
    public class IdentityDbSeeder
    {
        public static async Task SeedAsync(IdentificationDbContext context, IConfiguration _configuration)
        {
            if (!context.SubscriptionPlans.Any())
            {
                context.SubscriptionPlans.RemoveRange(context.SubscriptionPlans);
                context.SubscriptionPlans.AddRange(new List<SubscriptionPlan>
                {
                    new SubscriptionPlan()
                    {
                        Description = "The free subscription plan",
                        Name = "Free",
                        PlanType = SubscriptionPlanType.Free,
                        Price = 0
                    },
                    new SubscriptionPlan()
                    {
                        Description = "The premium subscription plan",
                        Name = "Premium",
                        PlanType = SubscriptionPlanType.Premium,
                        Price = 10,
                        StripePriceId = _configuration.GetSection("SubscriptionPlans")["PremiumStripeSubscriptionId"]
                    },
                    new SubscriptionPlan()
                    {
                        Description = "The enterprise subscription plan",
                        Name = "Enterprise",
                        PlanType = SubscriptionPlanType.Enterprise,
                        Price = 20,
                        StripePriceId = _configuration.GetSection("SubscriptionPlans")["EnterpriseStripeSubscriptionId"]
                    }
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
