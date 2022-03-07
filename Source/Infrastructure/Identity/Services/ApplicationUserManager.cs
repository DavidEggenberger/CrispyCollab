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

        public async Task<ApplicationUser> FindUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            ApplicationUser user = await base.GetUserAsync(claimsPrincipal);
            if(user == null)
            {
                throw new IdentityOperationException();
            }
            await identificationDbContext.Entry(user).Collection(u => u.Memberships).Query().Include(x => x.Team).LoadAsync();
            return user;
        }
        public async Task<ApplicationUser> FindUserByStripeCustomerId(string stripeCustomerId)
        {
            try
            {
                return await identificationDbContext.Users.SingleAsync(u => u.StripeCustomerId == stripeCustomerId);
            }
            catch(Exception ex)
            {
                throw new IdentityOperationException();
            }
        }
        public async Task<Guid> GetSelectedTeamId(ApplicationUser applicationUser)
        {
            try
            {
                return applicationUser.Memberships.Single(x => x.SelectionStatus == UserSelectionStatus.Selected).Team.Id;
            }
            catch (Exception ex)
            {
                applicationUser.Memberships.Add(new ApplicationUserTeam
                {
                    Role = TeamRole.Admin,
                    SelectionStatus = UserSelectionStatus.Selected,
                    Team = new Team
                    {
                        Name = "Your Team",
                        Subscription = new Subscription
                        {
                            SubscriptionPlan = await subscriptionPlanManager.FindByPlanType(SubscriptionPlanType.Free),
                            Status = SubscriptionStatus.Active
                        }
                    }
                });
                await identificationDbContext.SaveChangesAsync();
                return await GetSelectedTeamId(applicationUser);
            }
            throw new IdentityOperationException();
        }
        public async Task SelectTeamForUser(ApplicationUser applicationUser, Team team)
        {
            applicationUser.Memberships.ForEach(x => x.SelectionStatus = UserSelectionStatus.NotSelected);
            applicationUser.Memberships.Single(m => m.TeamId == team.Id).SelectionStatus = UserSelectionStatus.Selected;
            await identificationDbContext.SaveChangesAsync();
        }
        public Task<List<ApplicationUserTeam>> GetAllTeamMemberships(ApplicationUser applicationUser)
        {
            return identificationDbContext.ApplicationUserTeams.Include(x => x.Team).Where(x => x.UserId == applicationUser.Id).ToListAsync();
        }
        public async Task<List<Claim>> GetMembershipClaimsForApplicationUser(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = await identificationDbContext.Users.Include(x => x.Memberships).FirstAsync(x => x.Id == applicationUser.Id);
            ApplicationUserTeam applicationUserTeam;
            try
            {
                applicationUserTeam = _applicationUser.Memberships.Where(x => x.SelectionStatus == UserSelectionStatus.Selected).Single();
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException();
            }
            await identificationDbContext.Entry(applicationUserTeam).Reference(x => x.Team).Query().Include(t => t.Subscription).ThenInclude(x => x.SubscriptionPlan).LoadAsync();
            List<Claim> claims = new List<Claim>
            {
                new Claim("TeamSubscriptionPlanType", applicationUserTeam.Team.Subscription.SubscriptionPlan.PlanType.ToString()),
                new Claim("TeamName", applicationUserTeam.Team.Name),
                new Claim(IdentityStringConstants.IdentityTeamIdClaimType, applicationUserTeam.TeamId.ToString()),
                new Claim(IdentityStringConstants.IdentityTeamRoleClaimType, applicationUserTeam.Role.ToString())
            };
            return claims;
        }
    }
}
