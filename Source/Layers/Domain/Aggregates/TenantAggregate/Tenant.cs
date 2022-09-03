using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;
using Infrastructure.Identity;

namespace Domain.Aggregates.TenantAggregate
{
    [AggregateRoot]
    public class Tenant : Entity
    {
        public override Guid TenantId { get => base.TenantId; }
        public string Name { get; set; }
        public SubscriptionPlanType SUbscriptionPlan { get; set; }
        public List<TenantMembership> Memberships { get; set; }
    }
}
