using Application.TenantAggregate.Queries;
using AutoMapper;
using Domain.Aggregates.TenantAggregate;
using Infrastructure.CQRS.Query;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Identity;
using WebShared.DTOs.Aggregates.Tenant;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IMapper mapper;
        private readonly IQueryDispatcher queryDispatcher;
        public IdentityController(SignInManager<ApplicationUser> signInManager, ApplicationUserManager applicationUserManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.applicationUserManager = applicationUserManager;
            this.mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<BFFUserInfoDTO> GetClaimsOfCurrentUser()
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

        [HttpGet("selectTenant/{TeamId}")]
        public async Task<ActionResult> SetTenantForCurrentUser(Guid tenantId, [FromQuery] string redirectUri)
        {
            ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);

            var tenantMembershipsOfUserQuery = new GetAllTenantMembershipsOfUser() { UserId = applicationUser.Id };
            var tenantMemberships = await queryDispatcher.DispatchAsync<GetAllTenantMembershipsOfUser, List<TenantMembership>>(tenantMembershipsOfUserQuery);

            if (tenantMemberships.Select(t => t.Tenant.Id).Contains(tenantId))
            {
                await applicationUserManager.SetTenantAsSelected(applicationUser, tenantId);
                await signInManager.RefreshSignInAsync(applicationUser);
            }
            else
            {
                throw new Exception();
            }

            return LocalRedirect(redirectUri ?? "/");
        }

        [HttpGet("Logout")]
        public async Task<ActionResult> LogoutCurrentUser([FromQuery] string redirectUri)
        {
            await signInManager.SignOutAsync();
            return LocalRedirect(redirectUri ?? "/");
        }
    }
}
