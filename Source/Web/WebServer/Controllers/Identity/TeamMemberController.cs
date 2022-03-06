using Common.Identity.Team.DTOs;
using Common.Misc.Attributes;
using Infrastructure.EmailSender;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Team team = await teamManager.FindTeamAsync(HttpContext.User);
            foreach (var email in inviteUserToGroupDTO.Emails)
            {
                ApplicationUser invitedUser = await applicationUserManager.FindByEmailAsync(email);
                if(invitedUser == null)
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
            catch(Exception ex)
            {

            }
            return Ok();
        }

        [HttpPost("changerole")]
        public async Task<ActionResult> ChangeRoleOfTeamMember()
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTeamMember()
        {
            return Ok();
        }
    }
}
