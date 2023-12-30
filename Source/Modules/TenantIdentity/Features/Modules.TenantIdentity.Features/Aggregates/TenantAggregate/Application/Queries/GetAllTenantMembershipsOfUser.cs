using Modules.TenantIdentity.Features.Aggregates.TenantAggregate;
using Shared.Features.CQRS.Query;
using System;
using System.Collections.Generic;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Application.Queries
{
    public class GetAllTenantMembershipsOfUser : IQuery<List<TenantMembership>>
    {
        public Guid UserId { get; set; }
    }
}
