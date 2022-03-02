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
        private SignInManager<ApplicationUser> signInManager;
        public TeamController(TeamManager TeamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            this.teamManager = TeamManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
        }

        [HttpGet("current")]
        public async Task<ActionResult<TeamDTO>> GetSelectedTeamForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            IdentityOperationResult<Team> result = await applicationUserManager.GetSelectedTeam(applicationUser);
            if (result.Successful is false)
            {
                return NoContent();
            }
            return Ok(new TeamDTO { Name = result.Value.NameIdentitifer, Id = result.Value.Id, IconUrl = "https://icon" });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<TeamDTO>>> GetAllTeamsForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            IdentityOperationResult<List<ApplicationUserTeam>> result = await applicationUserManager.GetAllTeamMemberships(applicationUser);
            if (result.Successful is false)
            {
                return NoContent();
            }
            List<TeamDTO> teams = result.Value.Select(x => new TeamDTO { Name = x.Team.NameIdentitifer, Id = x.TeamId, IconUrl = "https://icon" }).ToList();
            return Ok(teams);
        }

        [HttpPost]
        public async Task<ActionResult<TeamDTO>> CreateTeam(CreateTeamDto createTeamDto)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            await applicationUserManager.UnSelectAllTeams(applicationUser);
            IdentityOperationResult result = await teamManager.CreateNewTeamAsync(applicationUser, createTeamDto.Name);
            if(result.Successful is false)
            {
                return Ok();
            }
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(applicationUser, true);
            return Ok();
        }    

        [HttpGet("setCurrent/{TeamId}")]
        public async Task<ActionResult> SetCurrentTeamForUser(Guid TeamId)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            
            applicationUser.Memberships.Where(x => x.TeamId == TeamId).First().Status = UserSelectionStatus.Selected;
            await identificationDbContext.SaveChangesAsync();
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(applicationUser, true);
            return Redirect("/");
        }
    }
}
