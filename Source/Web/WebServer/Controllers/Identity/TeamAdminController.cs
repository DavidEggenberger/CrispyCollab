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
        public TeamAdminController(TeamManager teamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            this.teamManager = teamManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
        }

        [HttpPost("invite")]
        public async Task<ActionResult> InviteUsersToTeam(InviteTeamMembersDTO inviteUserToGroupDTO, [FromServices] IHubContext<NotificationHub> hubContext)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            Team team = await teamManager.FindTeamAsync(HttpContext.User);
            foreach (var email in inviteUserToGroupDTO.Emails)
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
            try
            {
                await identificationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            await hubContext.Clients.Group($"{team.Id}{TeamRole.Admin}").SendAsync("UpdateTeam");
            return Ok();
        }

        [HttpPost("changerole")]
        public async Task<ActionResult> ChangeRoleOfTeamMember()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TeamDTO>> DeleteTeam(Guid id)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            Team team = await teamManager.FindTeamAsync(HttpContext.User);
            await teamManager.DeleteTeam(team);
            await signInManager.RefreshSignInAsync(applicationUser);
            return Ok();
        }
    }
}
