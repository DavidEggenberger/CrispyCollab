using Common.DTOs.Identity.Tenant;
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

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly TenantManager tenantManager;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IdentificationDbContext identificationDbContext;
        public TenantController(TenantManager tenantManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext)
        {
            this.tenantManager = tenantManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
        }

        [HttpGet("current")]
        public async Task<ActionResult<TenantDTO>> GetCurrentTenantForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);

            return Ok();
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<TenantDTO>>> GetAllTenantsForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            var t = await applicationUserManager.GetAllTenantMemberships(applicationUser);
            return Ok(t.Value.Select(x => new TenantDTO { Name = x.Tenant.NameIdentitifer, Id = x.Id, IconUrl = "https://icon" }).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<TenantDTO>> CreateTenant(CreateTenantDto createTenantDto)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            applicationUser.Memberships.Add(new ApplicationUserTenant
            {
                Tenant = new Tenant
                {
                    NameIdentitifer = createTenantDto.Name,
                    IconData = Convert.FromBase64String(createTenantDto.Base64Data)
                },
                Status = TenantStatus.Selected
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

        [HttpPost("setCurrent")]
        public async Task<ActionResult<TenantDTO>> SetCurrentTenantForUser(TenantDTO createTenantDTO)
        {
            return Ok(new TenantDTO());
        }
    }
}
