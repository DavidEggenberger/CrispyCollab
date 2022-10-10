using Common.Authorization.Services;
using Common.DomainKernel.Tenant;
using Common.Exstensions;
using Infrastructure.Identity.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Identity.Services
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
