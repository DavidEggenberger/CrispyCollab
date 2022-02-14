using Infrastructure.Identity;
using Infrastructure.Identity.Types.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ITenantManager
    {
        Task<IdentityOperationResult<List<ApplicationUser>>> GetAllMembersAsync(Tenant tenant);
        Task<IdentityOperationResult> CreateNewTenantAsync(string name);
        Task<IdentityOperationResult> AddNewUserToTenantAsync(ApplicationUser user, Tenant tenant);
        Task<IdentityOperationResult> ChangeRoleOfUserInTenantAsync(ApplicationUser user, Tenant tenant, TenantRoleType tenantRoleType);
    }
}
