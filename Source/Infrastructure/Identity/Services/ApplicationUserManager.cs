using Infrastructure.Identity;
using Infrastructure.Identity.Types;
using Infrastructure.Identity.Types.Constants;
using Infrastructure.Identity.Types.Enums;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private IdentificationDbContext identificationDbContext;
        public ApplicationUserManager(IdentificationDbContext identificationDbContext, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<ApplicationUserManager> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.identificationDbContext = identificationDbContext;
        }

        public async Task<IdentityOperationResult<List<ApplicationUserTenant>>> GetAllTenantMemberships(ApplicationUser applicationUser)
        {
            return IdentityOperationResult<List<ApplicationUserTenant>>.Success(applicationUser.Memberships);
        }
        public async Task<IdentityOperationResult<List<Tenant>>> GetTenantsWhereApplicationUserIsMember(ApplicationUser applicationUser)
        {
            throw new Exception();
        }
        public async Task<IdentityOperationResult<List<Tenant>>> GetTenantsWhereApplicationUserIsAdmin(ApplicationUser applicationUser)
        {
            throw new Exception();
        }
        public async Task<IdentityOperationResult<List<Claim>>> GetMembershipClaimsForApplicationUser(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = await identificationDbContext.Users.Include(x => x.Memberships).FirstAsync(x => x.Id == applicationUser.Id);
            ApplicationUserTenant applicationUserTenant = _applicationUser.Memberships.Where(x => x.Status == TenantStatus.Selected).FirstOrDefault();
            if(applicationUserTenant == null)
            {
                return IdentityOperationResult<List<Claim>>.Fail("");
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(IdentityStringConstants.IdentityTenantIdClaimType, applicationUserTenant.TenantId.ToString()),
                new Claim(IdentityStringConstants.IdentityTenantRoleClaimType, applicationUserTenant.Role.ToString())
            };
            return IdentityOperationResult<List<Claim>>.Success(claims);
        }
    }
}
