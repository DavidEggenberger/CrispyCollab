using Infrastructure.Identity;
using Infrastructure.Identity.Types.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity.Types.Enums;

namespace Infrastructure.Identity.Services
{
    public class TenantManager
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

        public async Task<IdentityOperationResult> InviteUserToRoleThroughEmail(Tenant tenant, TenantRoleType role, string Email)
        {
            throw new Exception();
        }
        public async Task<IdentityOperationResult> InviteUserThroughEmail(Tenant tenant, string Email)
        {
            throw new Exception();
        }
        public async Task<IdentityOperationResult> CreateNewTenantAsync(string name)
        {

            identificationDbContext.Tenants.Add(new Tenant
            {
                NameIdentitifer = name,
                
            });
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult<List<ApplicationUser>>> GetAllMembersAsync(Tenant tenant)
        {
            Tenant _tenant = await identificationDbContext.Tenants.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == tenant.Id);
            return IdentityOperationResult<List<ApplicationUser>>.Success(tenant.Members.Select(x => x.User).ToList());
        }
        public async Task<IdentityOperationResult<List<ApplicationUser>>> GetAllMembersByRoleAsync(Tenant tenant, TenantRoleType role)
        {
            Tenant _tenant = await identificationDbContext.Tenants.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == tenant.Id);
            return IdentityOperationResult<List<ApplicationUser>>.Success(tenant.Members.Where(x => x.Role == role).Select(x => x.User).ToList());
        }
        public async Task<IdentityOperationResult> UpdateTenantNameAsync(Tenant tenant, string newName)
        {
            throw new Exception();
        }
        public async Task<bool> CheckIfNameIsValidForTenantAsync(string name)
        {
            if(!identificationDbContext.Tenants.Any(x => x.NameIdentitifer == name))
            {
                return true;
            }
            return false;
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
            if (CheckTenantMembershipOfApplicationUser(applicationUser, tenant))
            {
                applicationUser.Memberships.ForEach(x => x.Status = TenantStatus.NotSelected);
                applicationUser.Memberships.Where(x => x.TenantId == tenant.Id).First().Status = TenantStatus.Selected;
                await identificationDbContext.SaveChangesAsync();
                await signInManager.RefreshSignInAsync(applicationUser);
                return IdentityOperationResult.Success();
            }
            return IdentityOperationResult.Fail("User is not a member of the tenant");
        }
        public async Task<IdentityOperationResult<Tenant>> GetCurrentSelectedTenantForApplicationUserAsync(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = identificationDbContext.Users.Include(x => x.Memberships).ThenInclude(x => x.Tenant).Where(x => x.Id == applicationUser.Id).FirstOrDefault();
            Tenant tenant = _applicationUser.Memberships.Where(x => x.Status == TenantStatus.Selected).First().Tenant;
            if (tenant != null)
            {
                return IdentityOperationResult<Tenant>.Success(tenant);
            }
            return IdentityOperationResult<Tenant>.Fail("User is not a member of the tenant");
        }
        public bool CheckTenantMembershipOfApplicationUser(ApplicationUser applicationUser, Tenant tenant)
        {
            if (applicationUser.Memberships.Any(x => x.TenantId == tenant.Id))
            {
                return true;
            }
            return false;
        }
        public IdentityOperationResult<TenantRoleType> GetTenantRoleOfApplicationUser(ApplicationUser applicationUser, Tenant tenant)
        {
            if (CheckTenantMembershipOfApplicationUser(applicationUser, tenant))
            {
                return IdentityOperationResult<TenantRoleType>.Success(applicationUser.Memberships.Single(x => x.TenantId == tenant.Id).Role);
            }
            return IdentityOperationResult<TenantRoleType>.Fail("User is not a member of the tenant");
        }
    }
}
