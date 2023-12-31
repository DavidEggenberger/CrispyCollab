using Modules.TenantIdentity.Features.Aggregates.UserAggregate;
using Shared.Features.CQRS.Query;
using Shared.SharedKernel.Constants;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Application.Queries;
using Modules.TenantIdentity.Features.Aggregates.TenantAggregate;

namespace Modules.TenantIdentity.Features.Aggregates.UserAggregate.Application.Queries
{
    public class GetClaimsForUser : IQuery<IEnumerable<Claim>>
    {
        public ApplicationUser User { get; set; }
    }

    public class GetClaimsForUserQueryHandler : IQueryHandler<GetClaimsForUser, IEnumerable<Claim>>
    {
        private readonly IQueryDispatcher queryDispatcher;
        public GetClaimsForUserQueryHandler(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }
        public async Task<IEnumerable<Claim>> HandleAsync(GetClaimsForUser query, CancellationToken cancellation)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimConstants.UserNameClaimType, query.User.UserName),
                new Claim(ClaimConstants.UserIdClaimType, query.User.Id.ToString()),
                new Claim(ClaimConstants.EmailClaimType, query.User.Email),
                new Claim(ClaimConstants.PictureClaimType, query.User.PictureUri)
            };

            var tenantByIdQuery = new GetTenantById() { TenantId = query.User.SelectedTenantId };
            Tenant currentTenant = await queryDispatcher.DispatchAsync<GetTenantById, Tenant>(tenantByIdQuery);

            var tenantMembershipQuery = new GetTenantMembershipQuery { TenantId = query.User.SelectedTenantId, UserId = query.User.Id };
            TenantMembership tenantMembership = await queryDispatcher.DispatchAsync<GetTenantMembershipQuery, TenantMembership>(tenantMembershipQuery);

            claims.AddRange(new List<Claim>
            {
                new Claim(ClaimConstants.TenantPlanClaimType, currentTenant.CurrentSubscriptionPlanType.ToString()),
                new Claim(ClaimConstants.TenantNameClaimType, currentTenant.Name),
                new Claim(ClaimConstants.TenantIdClaimType, currentTenant.TenantId.ToString()),
                new Claim(ClaimConstants.UserRoleInTenantClaimType, tenantMembership.Role.ToString()),
            });

            return claims;
        }
    }
}
