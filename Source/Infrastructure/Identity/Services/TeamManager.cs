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
        private ApplicationUserManager applicationUserManager;
        private SubscriptionPlanManager subscriptionPlanManager;

        public TeamManager(IdentificationDbContext identificationDbContext, ApplicationUserManager applicationUserManager, SubscriptionPlanManager subscriptionPlanManager)
        {
            this.identificationDbContext = identificationDbContext;
            this.applicationUserManager = applicationUserManager;
            this.subscriptionPlanManager = subscriptionPlanManager;
        }

        public async Task<Team> FindByClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal)
        {
            Team team;
            try
            {
                Guid id = new Guid(claimsPrincipal.FindFirst(x => x.Type == "TeamId").Value);
                team = await identificationDbContext.Teams.SingleAsync(t => t.Id == id);
            }
            catch (Exception ex)
            {
                return default;
            }
            await LoadTeamRelationsAsync(team);
            return team;
        }
        public async Task InviteMembersAsync(Team team, List<string> emails, TeamRole teamRole = TeamRole.User)
        {
            foreach (var email in emails)
            {
                ApplicationUser invitedUser = await applicationUserManager.FindByEmailAsync(email);
                if (invitedUser == null)
                {
                    ApplicationUser _applicationUser = new ApplicationUser
                    {
                        UserName = email,
                        Email = email
                    };
                    var result = await applicationUserManager.CreateAsync(_applicationUser);
                    if (result.Succeeded)
                    {
                        team.InviteMember(_applicationUser, TeamRole.User);
                    }
                }
                if ((invitedUser = await applicationUserManager.FindByEmailAsync(email)) != null && !team.Members.Any(m => m.UserId == invitedUser.Id))
                {
                    team.InviteMember(invitedUser, teamRole);
                }
            }
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task<Team> FindByIdAsync(Guid id)
        {
            Team team;
            try
            {
                team = await identificationDbContext.Teams.SingleAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new IdentityOperationException("No team is found");
            }
            await LoadTeamRelationsAsync(team);
            return team;
        }
        public async Task<Team> FindByIdAsync(string Id)
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
        public async Task CreateNewAsync(ApplicationUser applicationUser, string name)
        {
            if(identificationDbContext.Teams.Any(t => t.Name == name))
            {
                throw new IdentityOperationException("The Team name is taken");
            }
            var Subscription = new Subscription
            {
                SubscriptionPlan = await subscriptionPlanManager.FindByPlanType(SubscriptionPlanType.Free),
                Status = SubscriptionStatus.Active
            };
            Team team = new Team(applicationUser, Subscription, name);
            applicationUser.SelectedTeam = team;
            identificationDbContext.Teams.Add(team);    
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task UpdateNameAsync(Team team, string name)
        {
            if (identificationDbContext.Teams.Any(t => t.Name == name))
            {
                throw new IdentityOperationException("The Team name is taken");
            }
            team.Name = name;
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task AddMemberAsync(ApplicationUser user, Team Team, TeamRole teamRole = TeamRole.User)
        {
            Team.AddMember(user, teamRole);
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task ChangeRoleOfMemberAsync(ApplicationUser user, Team team, TeamRole teamRoleType)
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
        public async Task SetCurrentSelectedTeamForApplicationUserAsync(ApplicationUser applicationUser, Team Team)
        {
            applicationUser.SelectedTeam = Team;
            await identificationDbContext.SaveChangesAsync();   
        }
        public async Task<Team> GetSelectedTeamForApplicationUserAsync(ApplicationUser applicationUser)
        {
            try
            {
                return applicationUser.SelectedTeam;
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
        public async Task DeleteAsync(Team team)
        {
            identificationDbContext.Teams.Remove(identificationDbContext.Teams.Single(x => x.Id == team.Id));
            await identificationDbContext.SaveChangesAsync();
        }
        public async Task RemoveMemberAsync(Team team, ApplicationUser applicationUser)
        {
            team.RemoveMember(applicationUser);
            await identificationDbContext.SaveChangesAsync();
        }
        private async Task LoadTeamRelationsAsync(Team team)
        {
            await identificationDbContext.Entry(team).Collection(x => x.Notifications).LoadAsync();
            await identificationDbContext.Entry(team).Collection(t => t.Members).Query().Include(x => x.User).LoadAsync();
            await identificationDbContext.Entry(team).Reference(t => t.Subscription).Query().Include(x => x.SubscriptionPlan).LoadAsync();
        }
    }
}
