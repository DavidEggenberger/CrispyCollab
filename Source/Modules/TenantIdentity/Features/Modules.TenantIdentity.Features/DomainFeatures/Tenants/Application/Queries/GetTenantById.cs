using Shared.Features.Messaging.Query;
using System;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Queries
{
    public class GetTenantById : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
