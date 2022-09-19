using Common.Exstensions;
using Domain.Aggregates.TenantAggregate.Enums;
using Domain.Shared.DomainServices;
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
        public Role GetRoleOfUserInTenant()
        {
            return (Role)Enum.Parse(typeof(Role), httpContextAccessor.HttpContext.User.GetRoleClaim());
        }

        public void ThrowIfUserIsNotInRole(Role role)
        {
            if (httpContextAccessor.HttpContext.User.GetRoleClaim() != role.ToString())
            {
                throw new UnauthorizedException();
            }
        }
    }
}
