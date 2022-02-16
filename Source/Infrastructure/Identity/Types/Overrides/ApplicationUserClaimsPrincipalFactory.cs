﻿using Infrastructure.Services.TenantApplicationUserManager;
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
        private ApplicationUserManager applicationUserManager;
        public ApplicationUserClaimsPrincipalFactory(ApplicationUserManager tenantApplicationUserManager)
        {
            this.applicationUserManager = tenantApplicationUserManager;
        }
        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(user.Id.ToString());
            List<Claim> claims = new List<Claim>
            {

            };
            var result = await applicationUserManager.GetMembershipClaimsForUser(user);
            if (result.Successful)
            {
                claims.AddRange(result.Response);
            }

            //AuthenticationSheme (IdentityConstants.ApplicationSheme) gets set by ASP.NET Core Identity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
