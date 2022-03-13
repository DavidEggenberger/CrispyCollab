using AutoMapper;
using Common.Identity.DTOs.TeamDTOs;
using Common.Identity.Team.AdminManagement;
using Common.Identity.Team.DTOs;
using Common.Misc.Attributes;
using Infrastructure.Identity;
using Infrastructure.Identity.BusinessObjects;
using Infrastructure.Identity.Services;
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

namespace WebServer.Controllers.Identity.TeamControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeTeamAdmin]
    public class TeamAdminController : ControllerBase
    {
        private readonly TeamManager teamManager;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IdentificationDbContext identificationDbContext;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHubContext<NotificationHub> notificationHubContext;
        private readonly IMapper mapper;
        public TeamAdminController(TeamManager teamManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager, IHubContext<NotificationHub> notificationHubContext, IMapper mapper)
        {
            this.teamManager = teamManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
            this.notificationHubContext = notificationHubContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<TeamAdminInfoDTO> GetAdminInfo()
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            TeamMetrics teamMetrics = teamManager.GetMetricsForTeam(team);
            TeamAdminInfoDTO teamAdminInfoDTO = mapper.Map<TeamAdminInfoDTO>(team);
            teamAdminInfoDTO.Metrics = mapper.Map<TeamMetricsDTO>(teamMetrics);
            return teamAdminInfoDTO;
        }

        [HttpPost("invite")]
        public async Task<ActionResult> InviteUsers(InviteMembersDTO inviteUserToGroupDTO)
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            await teamManager.InviteMembersAsync(team, inviteUserToGroupDTO.Emails);   
            await notificationHubContext.Clients.Group($"{team.Id}{TeamRole.Admin}").SendAsync("UpdateTeam");
            return Ok();
        }

        [HttpPost("changerole")]
        public async Task<ActionResult> ChangeRoleOfTeamMember(ChangeRoleOfMemberDTO changeRoleOfTeamMemberDTO)
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(changeRoleOfTeamMemberDTO.UserId);
            await teamManager.ChangeRoleOfMemberAsync(applicationUser, team, (TeamRole)changeRoleOfTeamMemberDTO.TargetRole);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task DeleteTeam(Guid id)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
            Team team = await teamManager.FindByIdAsync(id);
            await teamManager.DeleteAsync(team);
            await signInManager.RefreshSignInAsync(applicationUser);
        }

        [HttpDelete("removeMember/{id}")]
        public async Task RemoveMember(Guid id)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(id);
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            await teamManager.RemoveMemberAsync(team, applicationUser);
        }
    }
}
