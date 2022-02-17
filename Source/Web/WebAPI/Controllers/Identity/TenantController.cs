using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        [HttpPost]
        public ActionResult T()
        {
            return Ok();
        }
    }
}
