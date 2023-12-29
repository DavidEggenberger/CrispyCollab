using Domain.Aggregates.TenantAggregate.Enums;

namespace Modules.TenantIdentity.Domain
{
    public class TenantInvitation : ValueObject
    {
        public Guid UserId { get; set; }
        public Role Role { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return Role;
        }
    }
}
