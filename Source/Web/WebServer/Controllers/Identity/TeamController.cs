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
using System.Linq;
using AutoMapper;

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
        private readonly IMapper mapper;
        public TeamController(TeamManager teamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            this.teamManager = teamManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<TeamDTO> GetSelectedTeamForUser()
        {
            Team team = await teamManager.FindTeamAsync(HttpContext.User);
            return mapper.Map<TeamDTO>(team);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<TeamDTO>> GetAllTeamsForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            List<ApplicationUserTeam> teamMemberships = applicationUserManager.GetAllTeamMemberships(applicationUser);
            return teamMemberships.Select(x => mapper.Map<TeamDTO>(x));
        }

        [HttpPost]
        public async Task<ActionResult<TeamDTO>> CreateTeam(TeamDTO team)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindUserAsync(HttpContext.User);
            await teamManager.CreateNewTeamAsync(applicationUser, team.Name);
            await signInManager.RefreshSignInAsync(applicationUser);
            return CreatedAtAction("CreateTeam", team);
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
