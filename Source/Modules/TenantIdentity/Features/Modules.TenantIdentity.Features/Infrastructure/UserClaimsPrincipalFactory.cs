using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Shared.Features.CQRS.Query;
using Modules.TenantIdentity.Features.Aggregates.UserAggregate.Application.Queries;
using Modules.TenantIdentity.Features.Aggregates.UserAggregate;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Kernel.BuildingBlocks.Auth.Constants;

namespace Modules.TenantIdentity.Features.Infrastructure
{
    public class UserClaimsPrincipalFactory<TUser> : IUserClaimsPrincipalFactory<TUser> where TUser : ApplicationUser
    {
        private readonly IQueryDispatcher queryDispatcher;
        public UserClaimsPrincipalFactory(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }

        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var claimsForUserQuery = new GetClaimsForUser { UserId = user.Id };
            var claimsForUser = await queryDispatcher.DispatchAsync<GetClaimsForUser, IEnumerable<Claim>>(claimsForUserQuery);
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsForUser, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.UserRoleInTenantClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
