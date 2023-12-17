using Shared.SharedKernel.Exstensions;
using Microsoft.AspNetCore.Http;
using Modules.IdentityModule.Domain.Exceptions;
using Shared.Kernel.BuildingBlocks.Authorization.Services;
using Shared.Kernel.BuildingBlocks.Auth.DomainKernel;

namespace Shared.Modules.Layers.Infrastructure.Identity.Services
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserAuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public TenantRole GetRoleOfUserInTenant()
        {
            return (TenantRole)Enum.Parse(typeof(TenantRole), httpContextAccessor.HttpContext.User.GetRoleClaim());
        }

        public void ThrowIfUserIsNotInRole(TenantRole role)
        {
            if (httpContextAccessor.HttpContext.User.GetRoleClaim() != role.ToString())
            {
                throw new UnauthorizedException();
            }
        }
    }
}
