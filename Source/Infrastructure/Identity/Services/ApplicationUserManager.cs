using Infrastructure.Identity;
using Infrastructure.Identity.Entities;
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
        private SubscriptionPlanManager subscriptionPlanManager;
        public ApplicationUserManager(SubscriptionPlanManager subscriptionPlanManager, IdentificationDbContext identificationDbContext, IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<ApplicationUserManager> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.identificationDbContext = identificationDbContext;
            this.subscriptionPlanManager = subscriptionPlanManager;
        }

        public List<ApplicationUserTeam> GetInvitedMemberships(ApplicationUser applicationUser)
        {
            return applicationUser.Memberships.Where(x => x.Role == TeamRole.Invited).ToList(); 
        }
        public async Task<ApplicationUser> FindByClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal)
        {
            ApplicationUser user = await base.GetUserAsync(claimsPrincipal);
            if(user == null)
            {
                throw new IdentityOperationException();
            }
            await LoadApplicationUserAsync(user);
            return user;
        }
        public async Task<ApplicationUser> FindByIdAsync(Guid id)
        {
            ApplicationUser applicationUser;
            try
            {
                applicationUser = await identificationDbContext.Users.SingleAsync(x => x.Id == id);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
            await LoadApplicationUserAsync(applicationUser);
            return applicationUser;
        }
        public async Task SetTeamAsSelected(ApplicationUser applicationUser, Team team)
        {
            applicationUser.SelectedTeam = team;
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task<ApplicationUser> FindUserByStripeCustomerId(string stripeCustomerId)
        {
            ApplicationUser applicationUser;
            try
            {
                applicationUser = await identificationDbContext.Users.SingleAsync(u => u.StripeCustomerId == stripeCustomerId);
                await LoadApplicationUserAsync(applicationUser);
                return applicationUser;
            }
            catch(Exception ex)
            {
                throw new IdentityOperationException();
            }
        }
        public List<Team> GetAllTeamsWhereUserIsMember(ApplicationUser applicationUser)
        {
            return applicationUser.Memberships.Where(x => x.UserId == applicationUser.Id).Select(x => x.Team).ToList();
        }
        public async Task<IEnumerable<Claim>> GetMembershipClaimsForApplicationUser(ApplicationUser applicationUser)
        {
            if(applicationUser.SelectedTeam != null)
            {
                ApplicationUserTeam applicationUserTeam = applicationUser.Memberships.Single(x => x.TeamId == applicationUser.SelectedTeam.Id);
                List<Claim> claims = new List<Claim>
                {
                    new Claim("TeamSubscriptionPlanType", applicationUserTeam.Team.Subscription.SubscriptionPlan.PlanType.ToString()),
                    new Claim("TeamName", applicationUserTeam.Team.Name),
                    new Claim(IdentityStringConstants.IdentityTeamIdClaimType, applicationUserTeam.TeamId.ToString()),
                    new Claim(IdentityStringConstants.IdentityTeamRoleClaimType, applicationUserTeam.Role.ToString())
                };
                return claims;
            }
            return Array.Empty<Claim>();
        }
        private async Task LoadApplicationUserAsync(ApplicationUser applicationUser)
        {
            await identificationDbContext.Entry(applicationUser).Reference(x => x.SelectedTeam).LoadAsync();
            await identificationDbContext.Entry(applicationUser).Collection(u => u.Memberships).Query()
                .Include(x => x.Team)
                .ThenInclude(x => x.Subscription)
                .ThenInclude(x => x.SubscriptionPlan).LoadAsync();
        }
    }
}
