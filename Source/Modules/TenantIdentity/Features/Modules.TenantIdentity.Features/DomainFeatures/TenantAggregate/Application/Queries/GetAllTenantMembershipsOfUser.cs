using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate;
using Shared.Features.Messaging.Query;
using System;
using System.Collections.Generic;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Queries
{
    public class GetAllTenantMembershipsOfUser : IQuery<List<TenantMembership>>
    {
        public Guid UserId { get; set; }
    }
}
