using Infrastructure.Identity;
using Infrastructure.Identity.Types;
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

namespace Infrastructure.Services.TenantApplicationUserManager
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private IdentificationDbContext identificationDbContext;
        public ApplicationUserManager(IdentificationDbContext identificationDbContext, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<ApplicationUserManager> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.identificationDbContext = identificationDbContext;
        }

        public async Task<IdentityOperationResult<List<Claim>>> GetMembershipClaimsForUser(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = await identificationDbContext.Users.Include(x => x.Memberships).FirstAsync(x => x.Id == applicationUser.Id);
            return new IdentityOperationResult<List<Claim>>
            {
                Successful = true,
                Response = _applicationUser.Memberships.Select(x => new Claim("Org1", "Admin")).ToList()
            };
        }
    }
}
