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
        public async Task InviteUserToRoleThroughEmail(Team team, TeamRole role, string email)
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
        }
        public Task InviteUserThroughEmail(Team team, string email)
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
        public async Task UpdateTeamNameAsync(Team team, string name)
        {
            if (identificationDbContext.Teams.Any(t => t.Name == name))
            {
                throw new IdentityOperationException("The Team name is taken");
            }
            team.Name = name;
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task AddMemberToTeamAsync(ApplicationUser user, Team Team, TeamRole teamRole = TeamRole.User)
        {
            Team.Members.Add(new ApplicationUserTeam
            {
                User = user
            });
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task ChangeRoleOfUserInTeamAsync(ApplicationUser user, Team team, TeamRole teamRoleType)
        {
            ApplicationUserTeam applicationUserTeam;
            try
            {
                applicationUserTeam = team.Members.Single(x => x.TeamId == team.Id);
            }
            catch(Exception ex)
            {
                throw new IdentityOperationException("Could retrieve ApplicationUserTeam");
            }
            applicationUserTeam.Role = teamRoleType;
            await identificationDbContext.SaveChangesAsync();
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
        public async Task<Team> GetSelectedTeamForApplicationUserAsync(ApplicationUser applicationUser)
        {
            try
            {
                return applicationUser.Memberships.Single(x => x.SelectionStatus == UserSelectionStatus.Selected).Team;
            }
            catch(Exception ex)
            {
                throw new IdentityOperationException("Selected Team couldn't be retrieved");
            }
        }
        public bool CheckTeamMembershipOfApplicationUser(ApplicationUser applicationUser, Team Team)
        {
            if (applicationUser.Memberships.Any(x => x.TeamId == Team.Id))
            {
                return true;
            }
            return false;
        }
        public TeamRole GetTeamRoleOfApplicationUser(ApplicationUser applicationUser, Team Team)
        {
            try
            {
                return applicationUser.Memberships.Single(x => x.TeamId == Team.Id).Role;
            }
            catch(Exception ex)
            {
                throw new IdentityOperationException("User is no member of the team");
            }
        }
        public async Task DeleteTeam(Team team)
        {
            identificationDbContext.Teams.Remove(identificationDbContext.Teams.Single(x => x.Id == team.Id));
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task RemoveMemberAsync(Team team, ApplicationUser applicationUser)
        {
            ApplicationUserTeam applicationUserTeam = GetCurrentMembershipForApplicationUser(applicationUser);
            
        }
        private ApplicationUserTeam GetCurrentMembershipForApplicationUser(ApplicationUser applicationUser)
        {
            try
            {
                return applicationUser.Memberships.Single(x => x.SelectionStatus == UserSelectionStatus.Selected);
            }
            catch(Exception ex)
            {
                throw new IdentityOperationException("No Membership could be found");
            }
        }
        private ApplicationUserTeam GetTeamMembershiForApplicationUser(ApplicationUser applicationUser, Team team)
        {
            try
            {
                return applicationUser.Memberships.Single(x => x.SelectionStatus == UserSelectionStatus.Selected);
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException("No Membership could be found");
            }
        }
        private async Task LoadTeamRelationsAsync(Team team)
        {
            await identificationDbContext.Entry(team).Collection(t => t.Members).Query().Include(x => x.User).LoadAsync();
            await identificationDbContext.Entry(team).Reference(t => t.Subscription).Query().Include(x => x.SubscriptionPlan).LoadAsync();
        }
    }
}
