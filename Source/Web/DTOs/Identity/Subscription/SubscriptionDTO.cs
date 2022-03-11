using Common.Identity.Team.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity.Subscription
{
    public class SubscriptionDTO
    {
        public SubscriptionStatusDTO SubscriptionStatus { get; set; }
        public SubscriptionPlanTypeDTO SubscriptionPlanType { get; set; }
    }
}
