using Infrastructure.Services.TenantApplicationUserManager;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Types.Overrides
{
    public class ApplicationUserClaimsPrincipalFactory<User> : IUserClaimsPrincipalFactory<User> where User : ApplicationUser
    {
        private TenantApplicationUserManager tenantApplicationUserManager;
        public ApplicationUserClaimsPrincipalFactory(TenantApplicationUserManager tenantApplicationUserManager)
        {
            this.tenantApplicationUserManager = tenantApplicationUserManager;
        }
        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            List<Claim> claims = user.GetClaims();
            var result = await tenantApplicationUserManager.GetMembershipClaimsForUser(user);
            if (result.Successful)
            {
                claims.AddRange(result.Response);
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "application");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
