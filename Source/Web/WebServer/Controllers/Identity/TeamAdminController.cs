using Common.Identity.DTOs.TeamDTOs;
using Common.Identity.Team.DTOs;
using Common.Misc.Attributes;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Hubs;

namespace WebServer.Controllers.Identity.TeamControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeTeamAdmin]
    public class TeamAdminController : ControllerBase
    {
        private readonly TeamManager teamManager;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IdentificationDbContext identificationDbContext;
        private readonly SignInManager<ApplicationUser> signInManager;
        private IHubContext<NotificationHub> notificationHubContext;
        public TeamAdminController(TeamManager teamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager, IHubContext<NotificationHub> notificationHubContext)
        {
            this.teamManager = teamManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
            this.notificationHubContext = notificationHubContext;
        }

        [HttpPost("invite")]
        public async Task<ActionResult> InviteUsersToTeam(InviteTeamMembersDTO inviteUserToGroupDTO)
        {
            Team team = await teamManager.FindTeamAsync(HttpContext.User);
            await teamManager.InviteUsersToTeam(team, inviteUserToGroupDTO.Emails);   
            await notificationHubContext.Clients.Group($"{team.Id}{TeamRole.Admin}").SendAsync("UpdateTeam");
            return Ok();
        }

        [HttpPost("changerole")]
        public async Task<ActionResult> ChangeRoleOfTeamMember(ChangeRoleOfTeamMemberDTO changeRoleOfTeamMemberDTO)
        {
            Team team = await teamManager.FindTeamAsync(HttpContext.User);
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(changeRoleOfTeamMemberDTO.UserId);
            await teamManager.ChangeRoleOfUserInTeamAsync(applicationUser, team, (TeamRole)changeRoleOfTeamMemberDTO.TargetRole);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TeamDTO>> DeleteTeam(Guid id)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            Team team = await teamManager.FindTeamByIdAsync(id);
            await teamManager.DeleteTeam(team);
            await signInManager.RefreshSignInAsync(applicationUser);
            return Ok();
        }

        [HttpDelete("removeMember/{id}")]
        public async Task<ActionResult<TeamDTO>> RemoveMember(Guid id)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(id);
            Team team = await teamManager.FindTeamAsync(HttpContext.User);
            await teamManager.RemoveMemberAsync(team, applicationUser);
            return Ok();
        }
    }
}
