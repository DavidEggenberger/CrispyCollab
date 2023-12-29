using Modules.IdentityModule.Domain.Exceptions;

namespace Shared.Modules.Layers.Features.Identity.Services
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
