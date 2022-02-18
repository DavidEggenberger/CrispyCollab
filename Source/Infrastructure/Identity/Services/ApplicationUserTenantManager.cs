using Infrastructure.Identity.Types.Enums;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class ApplicationUserTenantManager
    {
        private IdentificationDbContext identificationDbContext;
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;
        public ApplicationUserTenantManager(IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
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
            ApplicationUserTenant _tenantUser = _tenant.Members.First(x => x.UserId == user.Id);
            if (_tenantUser != null)
            {
                return IdentityOperationResult.Fail("User is already member of the tenant");
            }
            _tenant.Members.Add(new ApplicationUserTenant
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
            ApplicationUserTenant _tenantUser = _tenant.Members.First(x => x.UserId == user.Id);
            if (_tenantUser == null)
            {
                return IdentityOperationResult.Fail("User doesnt exist in tenant");
            }
            _tenantUser.Role = tenantRoleType;
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult> SetCurrentSelectedTenantForApplicationUserAsync(ApplicationUser applicationUser, Tenant tenant)
        {
            if(CheckTenantMembershipOfApplicationUser(applicationUser, tenant))
            {
                applicationUser.Memberships.ForEach(x => x.Status = TenantStatus.NotSelected);
                applicationUser.Memberships.Where(x => x.TenantId == tenant.Id).First().Status = TenantStatus.Selected;
                await identificationDbContext.SaveChangesAsync();
                await signInManager.RefreshSignInAsync(applicationUser);
                return IdentityOperationResult.Success();
            }
            return IdentityOperationResult.Fail("User is not a member of the tenant");
        }
        public bool CheckTenantMembershipOfApplicationUser(ApplicationUser applicationUser, Tenant tenant)
        {
            if(applicationUser.Memberships.Any(x => x.TenantId == tenant.Id))
            {
                return true; 
            }
            return false;
        }
        public IdentityOperationResult<TenantRoleType> GetTenantRoleOfApplicationUser(ApplicationUser applicationUser, Tenant tenant)
        {
            if(CheckTenantMembershipOfApplicationUser(applicationUser, tenant))
            {
                return IdentityOperationResult<TenantRoleType>.Success(applicationUser.Memberships.Single(x => x.TenantId == tenant.Id).Role);
            }
            return IdentityOperationResult<TenantRoleType>.Fail("User is not a member of the tenant");
        }
    }
}
