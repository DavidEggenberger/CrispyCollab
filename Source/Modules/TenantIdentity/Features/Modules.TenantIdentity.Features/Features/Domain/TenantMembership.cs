using Domain.Aggregates.TenantAggregate.Enums;

namespace Modules.TenantIdentity.Domain
{
    public class TenantMembership : Entity
    {
        private TenantMembership() { }
        public TenantMembership(Guid userId, Role role)
        {
            UserId = userId;
            Role = role;
        }
        public Guid UserId { get; set; }
        public Tenant Tenant { get; set; }
        public Role Role { get; set; }
    }
}
