using Infrastructure.Identity;
using Infrastructure.Identity.Types.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class TenantManager : ITenantManager
    {
        private IdentificationDbContext identificationDbContext;
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;
        public TenantManager(IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IdentityOperationResult> AddNewUserToTenantAsync(ApplicationUser user, Tenant tenant)
        {
            Tenant _tenant = await identificationDbContext.Tenants.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == tenant.Id);
            if (_tenant == null)
            {
                return IdentityOperationResult.Fail("Invalid Tenant");
            }
            TenantApplicationUser _tenantUser = _tenant.Members.First(x => x.UserId == user.Id);
            if(_tenantUser != null)
            {
                return IdentityOperationResult.Fail("User is already member of the tenant");
            }
            _tenant.Members.Add(new TenantApplicationUser
            {
                User = user
            });
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }

        public async Task<IdentityOperationResult> ChangeRoleOfUserInTenantAsync(ApplicationUser user, Tenant tenant, TenantRoleType tenantRoleType)
        {
            Tenant _tenant = await identificationDbContext.Tenants.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == tenant.Id);
            if (_tenant == null)
            {
                return IdentityOperationResult.Fail("Invalid Tenant");
            }
            TenantApplicationUser _tenantUser = _tenant.Members.First(x => x.UserId == user.Id);
            if (_tenantUser == null)
            {
                return IdentityOperationResult.Fail("User doesnt exist in tenant");
            }
            _tenantUser.Role = tenantRoleType;
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }

        public async Task<IdentityOperationResult> CreateNewTenantAsync(string name)
        {
            identificationDbContext.Tenants.Add(new Tenant
            {
                Name = name
            });
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }

        public async Task<IdentityOperationResult<List<ApplicationUser>>> GetAllMembersAsync(Tenant tenant)
        {
            Tenant _tenant = await identificationDbContext.Tenants.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == tenant.Id);
            return new IdentityOperationResult<List<ApplicationUser>>
            {
                Successful = true,
                Response = _tenant.Members.Select(s => s.User).ToList()
            };
        }
    }
}
