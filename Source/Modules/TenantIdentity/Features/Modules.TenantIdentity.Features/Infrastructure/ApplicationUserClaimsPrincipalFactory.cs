using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Modules.TenantIdentity.Features.Aggregates.UserAggregate;
using Modules.TenantIdentity.Features.Aggregates.UserAggregate.Application.Queries;
using Shared.Features.CQRS.Query;
using Shared.SharedKernel.Constants;

namespace Modules.TenantIdentity.Layers.Features
{
    public class ApplicationUserClaimsPrincipalFactory<User> : IUserClaimsPrincipalFactory<User> where User : ApplicationUser
    {
        private readonly IQueryDispatcher queryDispatcher;

        public ApplicationUserClaimsPrincipalFactory(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }

        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var getUserById = new GetUserById
            {

            };

            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(user.Id);

            var claimsForUserQuery = new GetClaimsForUser { User = applicationUser };
            var claimsForUser = await queryDispatcher.DispatchAsync<GetClaimsForUser, IEnumerable<Claim>>(claimsForUserQuery);
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsForUser, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.UserRoleInTenantClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
