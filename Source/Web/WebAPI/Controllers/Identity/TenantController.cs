using Common.DTOs.Identity.Tenant;
using Infrastructure.Identity;
using Infrastructure.Identity.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly TenantManager tenantManager;
        private readonly ApplicationUserTenantManager applicationUserTenantManager;
        private readonly ApplicationUserManager applicationUserManager;
        public TenantController(TenantManager tenantManager, ApplicationUserManager applicationUserManager, ApplicationUserTenantManager applicationUserTenantManager)
        {
            this.tenantManager = tenantManager;
            this.applicationUserManager = applicationUserManager;
            this.applicationUserTenantManager = applicationUserTenantManager;
        }

        [HttpGet("current")]
        public async Task<ActionResult<TenantDTO>> GetCurrentTenantForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);


            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<TenantDTO>> CreateTenant()
        {
            return Ok();
        }    

        [HttpPost("setCurrent")]
        public async Task<ActionResult<TenantDTO>> SetCurrentTenantForUser(CreateTenantDTO createTenantDTO)
        {
            return Ok(new TenantDTO());
        }
    }
}
