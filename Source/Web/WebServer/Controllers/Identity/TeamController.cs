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
        public TeamController(TeamManager teamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            this.teamManager = teamManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<ActionResult<TeamDTO>> GetSelectedTeamForUser()
        {
            Team team = await teamManager.FindTeamAsync(HttpContext.User);
            return Ok(await team.MapToTeamExtendedDTO());
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<TeamDTO>>> GetAllTeamsForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            List<ApplicationUserTeam> teamMemberships = await applicationUserManager.GetAllTeamMemberships(applicationUser);
            return Ok(await teamMemberships.MapToListTeamDTO());
        }

        [HttpPost]
        public async Task<ActionResult<TeamDTO>> CreateTeam(CreateTeamDto createTeamDto)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            await applicationUserManager.UnSelectAllTeams(applicationUser);
            IdentityOperationResult result = await teamManager.CreateNewTeamAsync(applicationUser, createTeamDto.Name);
            if(result.Successful is false)
            {
                return Ok();
            }
            await signInManager.RefreshSignInAsync(applicationUser);
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

        [HttpGet("select/{TeamId}")]
        public async Task<ActionResult> SetCurrentTeamForUser(Guid teamId)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            await applicationUserManager.UnSelectAllTeams(applicationUser);
            Team team = await teamManager.FindByIdAsync(teamId);
            await applicationUserManager.SelectTeamForUser(applicationUser, team);
            await signInManager.RefreshSignInAsync(applicationUser);
            return Redirect("/");
        }
    }
}
