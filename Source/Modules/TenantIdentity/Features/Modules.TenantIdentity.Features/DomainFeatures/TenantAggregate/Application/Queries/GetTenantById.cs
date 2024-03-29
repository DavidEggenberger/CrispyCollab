using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate;
using Shared.Features.CQRS.Query;
using System;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Queries
{
    public class GetTenantById : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
