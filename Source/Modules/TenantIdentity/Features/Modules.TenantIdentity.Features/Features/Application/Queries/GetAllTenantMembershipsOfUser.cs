using Modules.TenantIdentity.Domain;

namespace Shared.Modules.TenantIdentity.Application.Queries
{
    public class GetAllTenantMembershipsOfUser : IQuery<List<TenantMembership>>
    {
        public Guid UserId { get; set; }
    }
}
