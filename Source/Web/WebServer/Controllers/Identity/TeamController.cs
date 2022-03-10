using Common.DTOs.Identity.Team;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Common.Identity.DTOs.TeamDTOs;
using WebServer.Mappings;

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
        public async Task<TeamDTO> GetSelectedTeamForUser()
        {
            Team team = await teamManager.FindTeamAsync(HttpContext.User);
            return team.MapToTeamDTO();
        }

        [HttpGet("all")]
        public async Task<List<TeamDTO>> GetAllTeamsForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            List<ApplicationUserTeam> teamMemberships = applicationUserManager.GetAllTeamMemberships(applicationUser);
            return Ok(await teamMemberships.MapToListTeamDTO());
        }

        [HttpPost]
        public async Task<ActionResult<TeamDTO>> CreateTeam(CreateTeamDto createTeamDto)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            await teamManager.CreateNewTeamAsync(applicationUser, createTeamDto.Name);
            await signInManager.RefreshSignInAsync(applicationUser);
            return CreatedAtAction("CreateTeam", createTeamDto);
        }

        [HttpGet("select/{TeamId}")]
        public async Task<ActionResult> SetCurrentTeamForUser(Guid teamId, [FromQuery] string redirectUri)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            Team team = await teamManager.FindTeamByIdAsync(teamId);
            await applicationUserManager.SetTeamAsSelected(applicationUser, team);
            await signInManager.RefreshSignInAsync(applicationUser);
            return LocalRedirect(redirectUri ?? "/");
        }
    }
}
