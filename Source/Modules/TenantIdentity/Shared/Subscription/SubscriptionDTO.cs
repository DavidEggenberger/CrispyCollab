using WebShared.Identity.Team.DTOs.Enums;
using Modules.TenantIdentity.Web.DTOs.Enums;

namespace WebShared.Identity.Subscription
{
    public class SubscriptionDTO
    {
        public Guid Id { get; set; }
        public DateTime PeriodEnd { get; set; }
        public SubscriptionStatusDTO SubscriptionStatus { get; set; }
        public SubscriptionPlanTypeDTO SubscriptionPlanType { get; set; }
    }
}
