using Infrastructure.Identity;
using Infrastructure.Identity.Types;
using Infrastructure.Identity.Types.Constants;
using Infrastructure.Identity.Types.Enums;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Http;
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

        public async Task<IdentityOperationResult<Team>> GetSelectedTeam(ApplicationUser applicationUser)
        {
            Team team;
            if ((team = applicationUser.Memberships.SingleOrDefault(x => x.UserId == applicationUser.Id && x.Status == TeamStatus.Selected)?.Team) != null)
            {
                return IdentityOperationResult<Team>.Success(team);
            }
            return IdentityOperationResult<Team>.Fail("The User has no Team selected");
        }
        public async Task<IdentityOperationResult> UnSelectAllTeams(ApplicationUser applicationUser)
        {
            applicationUser.Memberships.ToList().ForEach(x => x.Status = TeamStatus.NotSelected);
            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult> SelectTeamForUser(ApplicationUser applicationUser, Team team)
        {
            applicationUser.Memberships.ToList().ForEach(x => x.Status = TeamStatus.NotSelected);
            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult<List<ApplicationUserTeam>>> GetAllTeamMemberships(ApplicationUser applicationUser)
        {
            return IdentityOperationResult<List<ApplicationUserTeam>>.Success(identificationDbContext.ApplicationUserTeams.Where(x => x.UserId == applicationUser.Id).ToList());
        }
        public async Task<IdentityOperationResult<List<Team>>> GetTeamsWhereApplicationUserIsMember(ApplicationUser applicationUser)
        {
            throw new Exception();
        }
        public async Task<IdentityOperationResult<List<Team>>> GetTeamsWhereApplicationUserIsAdmin(ApplicationUser applicationUser)
        {
            throw new Exception();
        }
        public async Task<IdentityOperationResult<List<Claim>>> GetMembershipClaimsForApplicationUser(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = await identificationDbContext.Users.Include(x => x.Memberships).FirstAsync(x => x.Id == applicationUser.Id);
            ApplicationUserTeam applicationUserTeam = _applicationUser.Memberships.Where(x => x.Status == TeamStatus.Selected).FirstOrDefault();
            if(applicationUserTeam == null)
            {
                return IdentityOperationResult<List<Claim>>.Fail("");
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(IdentityStringConstants.IdentityTeamIdClaimType, applicationUserTeam.TeamId.ToString()),
                new Claim(IdentityStringConstants.IdentityTeamRoleClaimType, applicationUserTeam.Role.ToString())
            };
            return IdentityOperationResult<List<Claim>>.Success(claims);
        }
    }
}
