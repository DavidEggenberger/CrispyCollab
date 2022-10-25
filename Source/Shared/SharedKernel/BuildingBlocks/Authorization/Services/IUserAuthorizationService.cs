using Shared.SharedKernel.DomainKernel.Tenant;

namespace Shared.SharedKernel.Authorization.Services
{
    public interface IUserAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
