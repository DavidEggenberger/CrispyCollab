using Common.DTOs.Identity.Tenant;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly TenantManager tenantManager;
        private readonly ApplicationUserManager applicationUserManager;
        public TenantController(TenantManager tenantManager, ApplicationUserManager applicationUserManager)
        {
            this.tenantManager = tenantManager;
            this.applicationUserManager = applicationUserManager;
        }

        [HttpGet("current")]
        public async Task<ActionResult<TenantInformationDTO>> GetCurrentTenantForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);

            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<TenantInformationDTO>> GetAllTenantsForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            var t = await applicationUserManager.GetAllTenantMemberships(applicationUser);
            return Ok(t.Value);
        }

        [HttpPost]
        public async Task<ActionResult<TenantInformationDTO>> CreateTenant()
        {
            return Ok();
        }    

        [HttpPost("setCurrent")]
        public async Task<ActionResult<TenantInformationDTO>> SetCurrentTenantForUser(CreateTenantDTO createTenantDTO)
        {
            return Ok(new TenantInformationDTO());
        }
    }
}
