using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TeamManager teamManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserManager userManager;
        public UserController(SignInManager<ApplicationUser> signInManager, ApplicationUserManager userManager, TeamManager teamManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.teamManager = teamManager;
        }

        [HttpGet("BFF")]
        [AllowAnonymous]
        public ActionResult<BFFUserInfoDTO> GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BFFUserInfoDTO.Anonymous;
            }

            return new BFFUserInfoDTO()
            {
                Claims = User.Claims.Select(claim => new ClaimValueDTO { Type = claim.Type, Value = claim.Value }).ToList()
            };
        }

        [Authorize]
        [HttpGet("Logout")]
        public async Task<ActionResult> LogoutCurrentUser()
        {
            await signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
