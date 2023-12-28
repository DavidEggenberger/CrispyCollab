using Modules.TenantIdentity.Domain;
using Shared.Features.CQRS.Query;

namespace Shared.Modules.TenantIdentity.Application.Queries
{
    public class GetAllTenantMembershipsOfUser : IQuery<List<TenantMembership>>
    {
        public Guid UserId { get; set; }
    }
}
