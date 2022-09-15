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
        public SubscriptionPlanType SubscriptionPlan { get; set; }
        public IReadOnlyCollection<TenantMembership> Memberships => memberships.AsReadOnly();
        private List<TenantMembership> memberships = new List<TenantMembership>();
    }
}
