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

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamManager TeamManager;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IdentificationDbContext identificationDbContext;
        private SignInManager<ApplicationUser> signInManager;
        public TeamController(TeamManager TeamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            this.TeamManager = TeamManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
        }

        [HttpGet("current")]
        public async Task<ActionResult<TeamDTO>> GetCurrentTeamForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            return Ok(applicationUser.Memberships.Where(m => m.Status == TeamStatus.Selected).Select(x => new TeamDTO { Name = x.Team.NameIdentitifer, Id = x.TeamId, IconUrl = "https://icon" }).First());
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<TeamDTO>>> GetAllTeamsForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            var t = await applicationUserManager.GetAllTeamMemberships(applicationUser);
            return Ok(t.Value.Select(x => new TeamDTO { Name = x.Team.NameIdentitifer, Id = x.TeamId, IconUrl = "https://icon" }).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<TeamDTO>> CreateTeam(CreateTeamDto createTeamDto)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            applicationUser.Memberships.Add(new ApplicationUserTeam
            {
                Team = new Team
                {
                    NameIdentitifer = createTeamDto.Name,
                    IconData = Convert.FromBase64String(createTeamDto.Base64Data)
                },
                Status = TeamStatus.Selected
            });
            try
            {
                int i = await identificationDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
            return Ok();
        }    

        [HttpGet("setCurrent/{TeamId}")]
        public async Task<ActionResult> SetCurrentTeamForUser(Guid TeamId)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            applicationUser.Memberships.ToList().ForEach(x => x.Status = TeamStatus.NotSelected);
            applicationUser.Memberships.Where(x => x.TeamId == TeamId).First().Status = TeamStatus.Selected;
            await identificationDbContext.SaveChangesAsync();
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(applicationUser, true);
            return Redirect("/");
        }
    }
}
