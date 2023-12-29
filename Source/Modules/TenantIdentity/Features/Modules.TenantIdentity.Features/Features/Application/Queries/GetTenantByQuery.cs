using Modules.TenantIdentity.Domain;

namespace Shared.Modules.TenantIdentity.Application.Queries
{
    public class GetTenantByQuery : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
