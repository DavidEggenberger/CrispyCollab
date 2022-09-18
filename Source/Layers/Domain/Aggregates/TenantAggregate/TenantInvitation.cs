using Domain.Aggregates.TenantAggregate.Enums;
using Domain.Kernel;

namespace Domain.Aggregates.TenantAggregate
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
