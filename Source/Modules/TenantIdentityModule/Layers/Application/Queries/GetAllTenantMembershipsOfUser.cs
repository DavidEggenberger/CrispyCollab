using Modules.TenantIdentityModule.Domain;
using Shared.Modules.Layers.Application.CQRS.Query;
using Modules.TenantIdentityModule.Domain;
using Shared.Modules.Layers.Infrastructure.CQRS.Query;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class GetAllTenantMembershipsOfUser : IQuery<List<TenantMembership>>
    {
        public Guid UserId { get; set; }
    }
}
