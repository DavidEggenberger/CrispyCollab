using Infrastructure.Identity.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly TenantManager tenantManager;
        private readonly ApplicationUserTenantManager applicationUserTenantManager;
        
        public TenantController(TenantManager tenantManager, ApplicationUserTenantManager applicationUserTenantManager)
        {
            this.tenantManager = tenantManager;
            this.applicationUserTenantManager = applicationUserTenantManager;
        }

        [HttpPost]
        public async Task<ActionResult> GetCurrentTenant(Person person)
        {
            return Ok();
        }
    }
    public class Person
    {
        public string Name { get; set; }
    }
}
