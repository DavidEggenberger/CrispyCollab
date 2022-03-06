using Common.Identity.Team.DTOs;
using Common.Misc.Attributes;
using Infrastructure.EmailSender;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeTeamAdmin]
    public class TeamMemberController : ControllerBase
    {
        private readonly TeamManager teamManager;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IdentificationDbContext identificationDbContext;
        private readonly SignInManager<ApplicationUser> signInManager;
        public TeamMemberController(TeamManager teamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            this.teamManager = teamManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
        }

        [HttpPost("invite")]
        public async Task<ActionResult> InviteUsersToTeam(InviteTeamMembersDTO inviteUserToGroupDTO)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            Team selectedTeam = (await teamManager.GetCurrentSelectedTeamForApplicationUserAsync(applicationUser)).Value;
            foreach (var email in inviteUserToGroupDTO.Emails)
            {
                ApplicationUser invitedUser;
                if ((invitedUser = await applicationUserManager.FindByEmailAsync(email)) != null && !selectedTeam.Members.Any(m => m.UserId == invitedUser.Id))
                {
                    invitedUser.Memberships.Add(new ApplicationUserTeam
                    {
                        Role = TeamRole.Admin,
                        Team = selectedTeam
                    });
                    await emailSender.SendEmailAsync(email, "Invitation", "");
                }
            }
            await identificationDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("changerole")]
        public async Task<ActionResult> ChangeRoleOfTeamMember()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> ChangeRoleOfTeamMember()
        {
            return Ok();
        }
    }
}
