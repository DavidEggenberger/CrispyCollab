using Modules.TenantIdentity.Domain;
using Shared.Features.CQRS.Query;

namespace Shared.Modules.TenantIdentity.Application.Queries
{
    public class GetTenantByQuery : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
