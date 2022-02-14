using Infrastructure.Identity;
using Infrastructure.Identity.Types;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.TenantApplicationUserManager
{
    public class TenantApplicationUserManager : ITenantApplicationUserManager
    {
        private IdentificationDbContext identificationDbContext;
        public TenantApplicationUserManager(IdentificationDbContext identificationDbContext)
        {
            this.identificationDbContext = identificationDbContext;
        }

        public async Task<IdentityOperationResult<IEnumerable<Claim>>> GetMembershipClaimsForUser(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = await identificationDbContext.Users.Include(x => x.Memberships).FirstAsync(x => x.Id == applicationUser.Id);
            return new IdentityOperationResult<IEnumerable<Claim>>
            {
                Response = _applicationUser.Memberships.Select(x => new Claim("", "")).ToList()
            };
        }
    }
}
