using AutoMapper;
using WebShared.Identity.DTOs.TeamDTOs;
using WebShared.Identity.Team.AdminManagement;
using WebShared.Identity.Team.DTOs;
using WebShared.Misc.Attributes;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServer.Hubs;
using WebServer.Mappings;

//namespace WebServer.Controllers.Identity.TeamControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [AuthorizeTeamAdmin]
//    public class TeamController : ControllerBase
//    {
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
//}
