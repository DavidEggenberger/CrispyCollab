using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class TenantsController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        //    [AuthorizeTeamAdmin]
        public class TeamController : ControllerBase
        {
        //        private readonly TeamManager teamManager;
        //        private readonly ApplicationUserManager applicationUserManager;
        //        private readonly IdentificationDbContext identificationDbContext;
        //        private readonly SignInManager<ApplicationUser> signInManager;
        //        private readonly IHubContext<NotificationHub> notificationHubContext;
        //        private readonly IMapper mapper;
        //        public TeamController(TeamManager teamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager, IHubContext<NotificationHub> notificationHubContext, IMapper mapper)
        //        {
        //            this.teamManager = teamManager;
        //            this.applicationUserManager = applicationUserManager;
        //            this.identificationDbContext = identificationDbContext;
        //            this.signInManager = signInManager;
        //            this.notificationHubContext = notificationHubContext;
        //            this.mapper = mapper;
        //        }

        //        [AllowAnonymous]
        //        [HttpPost]
        //        public async Task<ActionResult<TeamDTO>> CreateTeam(TeamDTO team)
        //        {
        //            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
        //            await teamManager.CreateNewAsync(applicationUser, team.Name);
        //            await signInManager.RefreshSignInAsync(applicationUser);
        //            return CreatedAtAction("CreateTeam", team);
        //        }

        //        [HttpGet]
        //        public async Task<TeamAdminInfoDTO> GetAdminInfo()
        //        {
        //            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
        //            TeamMetrics teamMetrics = teamManager.GetMetricsForTeam(team);
        //            TeamAdminInfoDTO teamAdminInfoDTO = mapper.Map<TeamAdminInfoDTO>(team);
        //            teamAdminInfoDTO.Metrics = mapper.Map<TeamMetricsDTO>(teamMetrics);
        //            return teamAdminInfoDTO;
        //        }


        //[HttpGet("allTeams")]
        //public async Task<IEnumerable<TeamDTO>> GetAllTeamsForUser()
        //{
        //    ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
        //    List<Team> teamMemberships = applicationUserManager.GetAllTeamsWhereUserIsMember(applicationUser);
        //    return teamMemberships.Select(x => mapper.Map<TeamDTO>(x));
        //}

        //        [HttpPost("invite")]
        //        public async Task<ActionResult> InviteUsers(InviteMembersDTO inviteUserToGroupDTO)
        //        {
        //            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
        //            await teamManager.InviteMembersAsync(team, inviteUserToGroupDTO.Emails);   
        //            return Ok();
        //        }

        //        [HttpPost("invite/revoke")]
        //        public async Task<ActionResult> RevokeInvitation(RevokeInvitationDTO revokeInvitationDTO)
        //        {
        //            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
        //            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(revokeInvitationDTO.UserId);
        //            await teamManager.RemoveInvitationAsync(team, applicationUser);
        //            return Ok();
        //        }

        //        [HttpPost("changerole")]
        //        public async Task<ActionResult> ChangeRoleOfTeamMember(ChangeRoleOfMemberDTO changeRoleOfTeamMemberDTO)
        //        {
        //            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
        //            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(changeRoleOfTeamMemberDTO.UserId);
        //            await teamManager.ChangeRoleOfMemberAsync(applicationUser, team, (TeamRole)changeRoleOfTeamMemberDTO.TargetRole);
        //            return Ok();
        //        }

        //        [HttpDelete("{id}")]
        //        public async Task DeleteTeam(Guid id)
        //        {
        //            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
        //            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
        //            if (team.CreatorId == applicationUser.Id)
        //            {
        //                await teamManager.DeleteAsync(team);
        //            }
        //            await signInManager.RefreshSignInAsync(applicationUser);
        //        }

        //        [HttpDelete("removeMember/{id}")]
        //        public async Task RemoveMember(Guid id)
        //        {
        //            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(id);
        //            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
        //            await teamManager.RemoveMemberAsync(team, applicationUser);
        //        }
        //    }
        }
    }
}
