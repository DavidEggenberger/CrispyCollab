using AutoMapper;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace WebServer.Controllers.Identity
//{
//    [Route("api/[controller]")]
//    [Authorize]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly TeamManager teamManager;
//        private readonly SignInManager<ApplicationUser> signInManager;
//        private readonly ApplicationUserManager applicationUserManager;
//        private readonly IMapper mapper;
//        public UserController(SignInManager<ApplicationUser> signInManager, ApplicationUserManager applicationUserManager, TeamManager teamManager, IMapper mapper)
//        {
//            this.signInManager = signInManager;
//            this.applicationUserManager = applicationUserManager;
//            this.teamManager = teamManager;
//            this.mapper = mapper;
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public ActionResult<BFFUserInfoDTO> GetCurrentUser()
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return BFFUserInfoDTO.Anonymous;
//            }
//            return new BFFUserInfoDTO()
//            {
//                Claims = User.Claims.Select(claim => new ClaimValueDTO { Type = claim.Type, Value = claim.Value }).ToList()
//            };
//        }

//        [HttpGet("selectedTeam")]
//        public async Task<TeamDTO> GetSelectedTeamForUser()
//        {
//            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
//            return mapper.Map<TeamDTO>(team);
//        }

//        [HttpGet("allTeams")]
//        public async Task<IEnumerable<TeamDTO>> GetAllTeamsForUser()
//        {
//            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
//            List<Team> teamMemberships = applicationUserManager.GetAllTeamsWhereUserIsMember(applicationUser);
//            return teamMemberships.Select(x => mapper.Map<TeamDTO>(x));
//        }

//        [HttpGet("selectTeam/{TeamId}")]
//        public async Task<ActionResult> SetCurrentTeamForUser(Guid teamId, [FromQuery] string redirectUri)
//        {
//            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
//            Team team = await teamManager.FindByIdAsync(teamId);
//            await applicationUserManager.SetTeamAsSelected(applicationUser, team);
//            await signInManager.RefreshSignInAsync(applicationUser);
//            return LocalRedirect(redirectUri ?? "/");
//        }

//        [HttpGet("Logout")]
//        public async Task<ActionResult> LogoutCurrentUser()
//        {
//            await signInManager.SignOutAsync();
//            return Redirect("/");
//        }
//    }
//}
