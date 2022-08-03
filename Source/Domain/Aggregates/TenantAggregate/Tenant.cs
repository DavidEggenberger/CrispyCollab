using Domain.SharedKernel;
using Domain.SharedKernel.Attributes;

namespace Domain.Aggregates.TenantAggregate
{
    [AggregateRoot]
    public class Tenant : Entity
    {
        public string Name { get; set; }
        public List<TenantMembership> Memberships { get; set; }
    }
}
