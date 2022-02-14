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
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
        public TenantManager(IdentificationDbContext identificationDbContext, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IdentityOperationResult> AddNewUserToTenantAsync(User user, Tenant tenant)
        {
            Tenant _tenant = await identificationDbContext.Tenants.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == tenant.Id);
            if (_tenant == null)
            {
                return IdentityOperationResult.Fail("Invalid Tenant");
            }
            TenantUser _tenantUser = _tenant.Members.First(x => x.UserId == user.Id);
            if(_tenantUser != null)
            {
                return IdentityOperationResult.Fail("User is already member of the tenant");
            }
            _tenant.Members.Add(new TenantUser
            {
                User = user
            });
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }

        public async Task<IdentityOperationResult> ChangeRoleOfUserInTenantAsync(User user, Tenant tenant, TenantRoleType tenantRoleType)
        {
            Tenant _tenant = await identificationDbContext.Tenants.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == tenant.Id);
            if (_tenant == null)
            {
                return IdentityOperationResult.Fail("Invalid Tenant");
            }
            TenantUser _tenantUser = _tenant.Members.First(x => x.UserId == user.Id);
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

        public async Task<IdentityOperationResult<List<User>>> GetAllMembersAsync(Tenant tenant)
        {
            Tenant _tenant = await identificationDbContext.Tenants.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == tenant.Id);
            return new IdentityOperationResult<List<User>>
            {
                Successful = true,
                Response = _tenant.Members.Select(s => s.User).ToList()
            };
        }
    }
}
