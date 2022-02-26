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
using Microsoft.AspNetCore.Identity;

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
        private SignInManager<ApplicationUser> signInManager;
        public TenantController(TenantManager tenantManager, ApplicationUserManager applicationUserManager, IdentificationDbContext identificationDbContext, SignInManager<ApplicationUser> signInManager)
        {
            this.tenantManager = tenantManager;
            this.applicationUserManager = applicationUserManager;
            this.identificationDbContext = identificationDbContext;
            this.signInManager = signInManager;
        }

        [HttpGet("current")]
        public async Task<ActionResult<TenantDTO>> GetCurrentTenantForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            return Ok(applicationUser.Memberships.Where(m => m.Status == TenantStatus.Selected).Select(x => new TenantDTO { Name = x.Tenant.NameIdentitifer, Id = x.TenantId, IconUrl = "https://icon" }).First());
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<TenantDTO>>> GetAllTenantsForUser()
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            var t = await applicationUserManager.GetAllTenantMemberships(applicationUser);
            return Ok(t.Value.Select(x => new TenantDTO { Name = x.Tenant.NameIdentitifer, Id = x.TenantId, IconUrl = "https://icon" }).ToList());
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

        [HttpGet("setCurrent/{tenantId}")]
        public async Task<ActionResult> SetCurrentTenantForUser(Guid tenantId)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByIdAsync(HttpContext.User.FindFirst(ClaimTypes.Sid).Value);
            applicationUser.Memberships.ToList().ForEach(x => x.Status = TenantStatus.NotSelected);
            applicationUser.Memberships.Where(x => x.TenantId == tenantId).First().Status = TenantStatus.Selected;
            await identificationDbContext.SaveChangesAsync();
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(applicationUser, true);
            return Redirect("/");
        }
    }
}
