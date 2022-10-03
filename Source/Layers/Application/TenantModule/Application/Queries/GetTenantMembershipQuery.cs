using Modules.TenantModule.Domain;
using Infrastructure.CQRS.Query;
using System.Threading;
using Modules.TenantModule.Infrastructure.EFCore;

namespace Application.TenantAggregate.Queries
{
    public class GetTenantMembershipQuery : IQuery<TenantMembership>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
    public class GetTenantMembershipQueryHandler : BaseQueryHandler<TenantDbContext, Tenant>, IQueryHandler<GetTenantMembershipQuery, TenantMembership>
    {
        public GetTenantMembershipQueryHandler(TenantDbContext tenantDbContext) : base(tenantDbContext) { } 
        public async Task<TenantMembership> HandleAsync(GetTenantMembershipQuery query, CancellationToken cancellation)
        {
            return dbSet.First().Memberships.Single(m => m.UserId == query.UserId);
        }
    }
}
