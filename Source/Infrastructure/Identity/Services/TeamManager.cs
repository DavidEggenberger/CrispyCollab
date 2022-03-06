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
using Infrastructure.Identity.Entities;

namespace Infrastructure.Identity.Services
{
    public class TeamManager
    {
        private IdentificationDbContext identificationDbContext;
        private SignInManager<ApplicationUser> signInManager;
        private ApplicationUserManager applicationUserManager;
        private SubscriptionPlanManager subscriptionPlanManager;
        public TeamManager(IdentificationDbContext identificationDbContext, ApplicationUserManager applicationUserManager, SignInManager<ApplicationUser> signInManager, SubscriptionPlanManager subscriptionPlanManager)
        {
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
            this.applicationUserManager = applicationUserManager;
            this.subscriptionPlanManager = subscriptionPlanManager;
        }

        public async Task<SubscriptionPlanType> GetSubscriptionPlanTypeAsync(Team team)
        {
            await identificationDbContext.Entry(team).Reference(x => x.Subscription).LoadAsync();
            await identificationDbContext.Entry(team.Subscription).Reference(x => x.SubscriptionPlan).LoadAsync();
            return team.Subscription.SubscriptionPlan.PlanType;
        }
        public async Task<List<ApplicationUserTeam>> GetMembersAsync(Team team)
        {
            Team _team = await identificationDbContext.Teams.Include(x => x.Members).ThenInclude(x => x.User).SingleOrDefaultAsync(t => t.Id == team.Id);
            return _team.Members.ToList();
        }
        public async Task<IdentityOperationResult> InviteUserToRoleThroughEmail(Team team, TeamRole role, string email)
        {
            ApplicationUser applicationUser;
            if((applicationUser = await applicationUserManager.FindByEmailAsync(email)) != null)
            {
                if(!team.Members.Any(x => x.UserId == applicationUser.Id))
                {
                    team.Members.Add(new ApplicationUserTeam
                    {
                        Role = TeamRole.Invited,
                        User = applicationUser
                    });
                }
            }
            else
            {
                team.Members.Add(new ApplicationUserTeam
                {
                    Role = TeamRole.Invited,
                    User = new ApplicationUser
                    {
                        Email = email
                    }
                });
            }
            await identificationDbContext.SaveChangesAsync();
            return null;
        }
        public Task<IdentityOperationResult> InviteUserThroughEmail(Team team, string email)
        {
            return InviteUserToRoleThroughEmail(team, TeamRole.User, email);
        }
        public Task<Team> FindByIdAsync(Guid Id)
        {
            return identificationDbContext.Teams.SingleOrDefaultAsync(x => x.Id == Id);
        }
        public Task<Team> FindByIdAsync(string Id)
        {
            return identificationDbContext.Teams.SingleOrDefaultAsync(x => x.Id == new Guid(Id));
        }
        public async Task LoadExtendedTeamInformationAsync(Team team)
        {
            await identificationDbContext.Entry(team).Collection(x => x.Members).LoadAsync();
            await identificationDbContext.Entry(team).Reference(x => x.Subscription).LoadAsync();
            await identificationDbContext.Entry(team.Subscription).Reference(x => x.SubscriptionPlan).LoadAsync();
        }
        public Task<Team> FindUsersSelectedTeam(string Id)
        {
            return identificationDbContext.Teams.SingleOrDefaultAsync(x => x.Id == new Guid(Id));
        }
        public async Task<IdentityOperationResult> CreateNewTeamAsync(ApplicationUser applicationUser, string name)
        {
            var sapplicationUser = await identificationDbContext.Users.Include(x => x.Memberships).SingleOrDefaultAsync(x => x.Id == applicationUser.Id);
            if(identificationDbContext.Teams.Any(t => t.NameIdentitifer == name))
            {
                return IdentityOperationResult.Fail("The Team name is taken");
            }
            sapplicationUser.Memberships.Add(new ApplicationUserTeam
            {
                Team = new Team
                {
                    NameIdentitifer = name,
                    Subscription = new Subscription
                    {
                        SubscriptionPlan = await subscriptionPlanManager.FindByPlanType(SubscriptionPlanType.Free),
                        Status = SubscriptionStatus.Active
                    }
                },
                Role = TeamRole.Admin,
                Status = UserSelectionStatus.Selected
            });
            await identificationDbContext.SaveChangesAsync();

            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult<List<ApplicationUser>>> GetAllMembersAsync(Team Team)
        {
            Team _Team = await identificationDbContext.Teams.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == Team.Id);
            return IdentityOperationResult<List<ApplicationUser>>.Success(Team.Members.Select(x => x.User).ToList());
        }
        public async Task<IdentityOperationResult<List<ApplicationUser>>> GetAllMembersByRoleAsync(Team Team, TeamRole role)
        {
            Team _Team = await identificationDbContext.Teams.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == Team.Id);
            return IdentityOperationResult<List<ApplicationUser>>.Success(Team.Members.Where(x => x.Role == role).Select(x => x.User).ToList());
        }
        public async Task<IdentityOperationResult> UpdateTeamNameAsync(Team Team, string newName)
        {
            throw new Exception();
        }
        public async Task<bool> CheckIfNameIsValidForTeamAsync(string name)
        {
            if(!identificationDbContext.Teams.Any(x => x.NameIdentitifer == name))
            {
                return true;
            }
            return false;
        }
        public async Task<IdentityOperationResult> AddNewUserToTeamAsync(ApplicationUser user, Team Team)
        {
            Team _Team = await identificationDbContext.Teams.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == Team.Id);
            if (_Team == null)
            {
                return IdentityOperationResult.Fail("Invalid Team");
            }
            ApplicationUserTeam _TeamUser = _Team.Members.First(x => x.UserId == user.Id);
            if (_TeamUser != null)
            {
                return IdentityOperationResult.Fail("User is already member of the Team");
            }
            _Team.Members.Add(new ApplicationUserTeam
            {
                User = user
            });
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult> ChangeRoleOfUserInTeamAsync(ApplicationUser user, Team Team, TeamRole TeamRoleType)
        {
            Team _Team = await identificationDbContext.Teams.Include(x => x.Members).ThenInclude(x => x.User).FirstAsync(x => x.Id == Team.Id);
            if (_Team == null)
            {
                return IdentityOperationResult.Fail("Invalid Team");
            }
            ApplicationUserTeam _TeamUser = _Team.Members.First(x => x.UserId == user.Id);
            if (_TeamUser == null)
            {
                return IdentityOperationResult.Fail("User doesnt exist in Team");
            }
            _TeamUser.Role = TeamRoleType;
            await identificationDbContext.SaveChangesAsync();
            return IdentityOperationResult.Success();
        }
        public async Task<IdentityOperationResult> SetCurrentSelectedTeamForApplicationUserAsync(ApplicationUser applicationUser, Team Team)
        {
            if (CheckTeamMembershipOfApplicationUser(applicationUser, Team))
            {
                applicationUser.Memberships.ForEach(x => x.Status = UserSelectionStatus.NotSelected);
                applicationUser.Memberships.Where(x => x.TeamId == Team.Id).First().Status = UserSelectionStatus.Selected;
                await identificationDbContext.SaveChangesAsync();
                await signInManager.RefreshSignInAsync(applicationUser);
                return IdentityOperationResult.Success();
            }
            return IdentityOperationResult.Fail("User is not a member of the Team");
        }
        public async Task<IdentityOperationResult<Team>> GetCurrentSelectedTeamForApplicationUserAsync(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = identificationDbContext.Users.Include(x => x.Memberships).ThenInclude(x => x.Team).Where(x => x.Id == applicationUser.Id).FirstOrDefault();
            Team Team = _applicationUser.Memberships.Where(x => x.Status == UserSelectionStatus.Selected).First().Team;
            if (Team != null)
            {
                return IdentityOperationResult<Team>.Success(Team);
            }
            return IdentityOperationResult<Team>.Fail("User is not a member of the Team");
        }
        public bool CheckTeamMembershipOfApplicationUser(ApplicationUser applicationUser, Team Team)
        {
            if (applicationUser.Memberships.Any(x => x.TeamId == Team.Id))
            {
                return true;
            }
            return false;
        }
        public IdentityOperationResult<TeamRole> GetTeamRoleOfApplicationUser(ApplicationUser applicationUser, Team Team)
        {
            if (CheckTeamMembershipOfApplicationUser(applicationUser, Team))
            {
                return IdentityOperationResult<TeamRole>.Success(applicationUser.Memberships.Single(x => x.TeamId == Team.Id).Role);
            }
            return IdentityOperationResult<TeamRole>.Fail("User is not a member of the Team");
        }
    }
}
