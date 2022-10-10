using Common.DomainKernel.Tenant;

namespace Common.Authorization.Services
{
    public interface IUserAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
