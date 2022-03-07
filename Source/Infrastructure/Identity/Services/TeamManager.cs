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
using Infrastructure.Identity.Types;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

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

        public async Task<Team> FindTeamAsync(ClaimsPrincipal claimsPrincipal)
        {
            Team team;
            try
            {
                Guid id = new Guid(claimsPrincipal.FindFirst(x => x.Type == "TeamId").Value);
                team = await identificationDbContext.Teams.SingleAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException("No team is found");
            }
            await LoadTeamRelationsAsync(team);
            return team;
        }
        public async Task InviteUsersToTeam(Team team, List<string> emails)
        {
            foreach (var email in emails)
            {
                ApplicationUser invitedUser = await applicationUserManager.FindByEmailAsync(email);
                if (invitedUser == null)
                {
                    await applicationUserManager.CreateAsync(new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        Memberships = new List<ApplicationUserTeam>
                        {
                            new ApplicationUserTeam
                            {
                                Role = TeamRole.Invited,
                                Team = team
                            }
                        }
                    });
                }
                if ((invitedUser = await applicationUserManager.FindByEmailAsync(email)) != null && !team.Members.Any(m => m.UserId == invitedUser.Id))
                {
                    invitedUser.Memberships.Add(new ApplicationUserTeam
                    {
                        Role = TeamRole.Invited,
                        Team = team
                    });
                }
            }
            await identificationDbContext.SaveChangesAsync();
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
        public Task<Team> FindTeamByIdAsync(Guid id)
        {
            return FindTeamByIdAsync(id.ToString());
        }
        public async Task<Team> FindTeamByIdAsync(string Id)
        {
            Team team;
            try
            {
                team = await identificationDbContext.Teams.SingleOrDefaultAsync(x => x.Id == new Guid(Id));
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException("No Team is found");
            }
            await LoadTeamRelationsAsync(team);
            return team;
        }
        public Task<Team> FindUsersSelectedTeam(string Id)
        {
            return identificationDbContext.Teams.SingleOrDefaultAsync(x => x.Id == new Guid(Id));
        }
        public async Task CreateNewTeamAsync(ApplicationUser applicationUser, string name)
        {
            var sapplicationUser = await identificationDbContext.Users.Include(x => x.Memberships).SingleOrDefaultAsync(x => x.Id == applicationUser.Id);
            if(identificationDbContext.Teams.Any(t => t.Name == name))
            {
                throw new IdentityOperationException("The Team name is taken");
            }
            applicationUser.Memberships.ForEach(x => x.SelectionStatus = UserSelectionStatus.NotSelected);
            sapplicationUser.Memberships.Add(new ApplicationUserTeam
            {
                Team = new Team
                {
                    Name = name,
                    Subscription = new Subscription
                    {
                        SubscriptionPlan = await subscriptionPlanManager.FindByPlanType(SubscriptionPlanType.Free),
                        Status = SubscriptionStatus.Active
                    }
                },
                Role = TeamRole.Admin,
                SelectionStatus = UserSelectionStatus.Selected
            });
            await identificationDbContext.SaveChangesAsync();
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
        public async Task UpdateTeamNameAsync(Team team, string name)
        {
            if (identificationDbContext.Teams.Any(t => t.Name == name))
            {
                throw new IdentityOperationException("The Team name is taken");
            }
            team.Name = name;
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task<bool> CheckIfNameIsValidForTeamAsync(string name)
        {
            if(!identificationDbContext.Teams.Any(x => x.Name == name))
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
                applicationUser.Memberships.ForEach(x => x.SelectionStatus = UserSelectionStatus.NotSelected);
                applicationUser.Memberships.Where(x => x.TeamId == Team.Id).First().SelectionStatus = UserSelectionStatus.Selected;
                await identificationDbContext.SaveChangesAsync();
                await signInManager.RefreshSignInAsync(applicationUser);
                return IdentityOperationResult.Success();
            }
            return IdentityOperationResult.Fail("User is not a member of the Team");
        }
        public async Task<IdentityOperationResult<Team>> GetCurrentSelectedTeamForApplicationUserAsync(ApplicationUser applicationUser)
        {
            ApplicationUser _applicationUser = identificationDbContext.Users.Include(x => x.Memberships).ThenInclude(x => x.Team).Where(x => x.Id == applicationUser.Id).FirstOrDefault();
            Team Team = _applicationUser.Memberships.Where(x => x.SelectionStatus == UserSelectionStatus.Selected).First().Team;
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
        public async Task DeleteTeam(Team team)
        {
            identificationDbContext.Teams.Remove(identificationDbContext.Teams.Single(x => x.Id == team.Id));
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task RemoveMemberAsync(Team team, ApplicationUser applicationUser)
        {
            
        }
        public async Task LoadTeamRelationsAsync(Team team)
        {
            await identificationDbContext.Entry(team).Collection(t => t.Members).Query().Include(x => x.User).LoadAsync();
            await identificationDbContext.Entry(team).Reference(t => t.Subscription).Query().Include(x => x.SubscriptionPlan).LoadAsync();
        }
    }
}
