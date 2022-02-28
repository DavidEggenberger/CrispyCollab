using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class Subscription
    {

        public string StripeSubscriptionId { get; set; }
        public SubscriptionType PlanType { get; set; }
    }
}
