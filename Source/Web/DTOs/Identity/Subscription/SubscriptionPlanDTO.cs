using Common.Identity.Team.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.Subscription
{
    public class SubscriptionPlanDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int TrialPeriodDays { get; set; }
        public SubscriptionPlanTypeDTO PlanType { get; set; }
    }
}
