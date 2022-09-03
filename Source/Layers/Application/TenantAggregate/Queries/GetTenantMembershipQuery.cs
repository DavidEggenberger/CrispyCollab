using Domain.Aggregates.TenantAggregate;
using Infrastructure.CQRS.Query;
using Infrastructure.EFCore;
using System.Threading;

namespace Application.TenantAggregate.Queries
{
    public class GetTenantMembershipQuery : IQuery<TenantMembership>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
    public class GetTenantMembershipQueryHandler : IQueryHandler<GetTenantMembershipQuery, TenantMembership>
    {
        private readonly ApplicationDbContext applicationDbContext;
        public GetTenantMembershipQueryHandler(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<TenantMembership> HandleAsync(GetTenantMembershipQuery query, CancellationToken cancellation)
        {
            return applicationDbContext.Tenants.First().Memberships.Single(m => m.UserId == query.UserId);
        }
    }
}
