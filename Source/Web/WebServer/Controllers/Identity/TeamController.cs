using Common.DTOs.Identity.Team;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Infrastructure.Identity.Types.Enums;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity.Types.Shared;
using Common.Identity.Team.DTOs;
using Infrastructure.EmailSender;
using Common.Misc.Attributes;
using Common.Identity.Team.DTOs.Enums;
using Common.Identity.ApplicationUser;
using WebServer.Mappings.Identity;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamManager teamManager;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IdentificationDbContext identificationDbContext;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender emailSender;
        public TeamController(TeamManager teamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            this.teamManager = teamManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        [HttpGet("current")]
        public async Task<ActionResult<TeamDTO>> GetSelectedTeamForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.GetUserAsync(HttpContext.User);
            Guid teamId = await applicationUserManager.GetSelectedTeamId(applicationUser);
            Team team = await teamManager.FindByIdAsync(teamId);
            return Ok(await team.MapToTeamDTO());
        }

        [HttpGet("currentExtended")]
        [AuthorizeTeamAdmin]
        public async Task<ActionResult<TeamExtendedDTO>> GetExtendedSelectedTeamForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.GetUserAsync(HttpContext.User);
            Guid teamId = await applicationUserManager.GetSelectedTeamId(applicationUser);
            Team team = await teamManager.FindByIdAsync(teamId);
            return Ok(await team.MapToTeamExtendedDTO(identificationDbContext));
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<TeamDTO>>> GetAllTeamsForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.GetUserAsync(HttpContext.User);
            List<ApplicationUserTeam> teamMemberships = await applicationUserManager.GetAllTeamMemberships(applicationUser);
            return Ok(await teamMemberships.MapToListTeamDTO(identificationDbContext));
        }

        [HttpPost]
        public async Task<ActionResult<TeamDTO>> CreateTeam(CreateTeamDto createTeamDto)
        {
            ApplicationUser applicationUser = await applicationUserManager.GetUserAsync(HttpContext.User);
            await applicationUserManager.UnSelectAllTeams(applicationUser);
            IdentityOperationResult result = await teamManager.CreateNewTeamAsync(applicationUser, createTeamDto.Name);
            if(result.Successful is false)
            {
                return Ok();
            }
            await signInManager.RefreshSignInAsync(applicationUser);
            return Ok();
        }    

        [HttpGet("select/{TeamId}")]
        public async Task<ActionResult> SetCurrentTeamForUser(Guid teamId)
        {
            ApplicationUser applicationUser = await applicationUserManager.GetUserAsync(HttpContext.User);
            await applicationUserManager.UnSelectAllTeams(applicationUser);
            Team team = await teamManager.FindByIdAsync(teamId);
            await applicationUserManager.SelectTeamForUser(applicationUser, team);
            await signInManager.RefreshSignInAsync(applicationUser);
            return Redirect("/");
        }

        [HttpPost("invite")]
        [AuthorizeTeamAdmin]
        public async Task<ActionResult> InviteUsersToTeam(InviteUserToTeamDTO inviteUserToGroupDTO)
        {
            ApplicationUser applicationUser = await applicationUserManager.GetUserAsync(HttpContext.User);
            Team selectedTeam = (await teamManager.GetCurrentSelectedTeamForApplicationUserAsync(applicationUser)).Value;
            foreach (var email in inviteUserToGroupDTO.Emails)
            {
                ApplicationUser invitedUser;
                if((invitedUser = await applicationUserManager.FindByEmailAsync(email)) != null && !selectedTeam.Members.Any(m => m.UserId == invitedUser.Id))
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
    }
}
