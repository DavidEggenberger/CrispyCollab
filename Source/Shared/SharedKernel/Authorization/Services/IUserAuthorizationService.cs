using SharedKernel.DomainKernel;

namespace Shared.SharedKernel.Authorization.Services
{
    public interface IUserAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
