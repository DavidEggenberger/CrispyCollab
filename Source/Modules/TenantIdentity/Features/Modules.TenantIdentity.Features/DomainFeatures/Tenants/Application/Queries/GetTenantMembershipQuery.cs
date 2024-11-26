using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Query;
using Shared.Features.Server;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Queries
{
    public class GetTenantMembershipQuery : Query<TenantMembership>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
    public class GetTenantMembershipQueryHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetTenantMembershipQuery, TenantMembership>
    {
        public GetTenantMembershipQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<TenantMembership> HandleAsync(GetTenantMembershipQuery query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
