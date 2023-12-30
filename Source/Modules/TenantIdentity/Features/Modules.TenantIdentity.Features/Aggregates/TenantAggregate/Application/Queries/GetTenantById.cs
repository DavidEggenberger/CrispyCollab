using Modules.TenantIdentity.Features.Aggregates.TenantAggregate;
using Shared.Features.CQRS.Query;
using System;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Application.Queries
{
    public class GetTenantById : IQuery<Tenant>
    {
        public Guid TenantId { get; set; }
    }
}
