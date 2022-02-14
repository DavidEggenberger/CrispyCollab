using Infrastructure.Identity;
using Infrastructure.Identity.Types;
using Infrastructure.Identity.Types.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.TenantApplicationUserManager
{
    public interface ITenantApplicationUserManager
    {
        Task<IdentityOperationResult<IEnumerable<Claim>>> GetMembershipClaimsForUser(ApplicationUser applicationUser);
    }
}
