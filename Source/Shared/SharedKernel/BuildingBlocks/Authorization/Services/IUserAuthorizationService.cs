using SharedKernel.DomainKernel.Tenant;

namespace SharedKernel.Authorization.Services
{
    public interface IUserAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
