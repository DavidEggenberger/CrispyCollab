using Modules.TenantIdentity.Web.DTOs.Enums;

namespace WebShared.Identity.Subscription
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
