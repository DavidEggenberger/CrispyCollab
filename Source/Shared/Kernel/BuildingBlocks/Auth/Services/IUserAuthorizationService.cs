using Shared.Kernel.DomainKernel;

namespace Shared.Kernel.BuildingBlocks.Auth.Services
{
    public interface IUserAuthorizationService
    {
        void ThrowIfUserIsNotInRole(TenantRole role);
        TenantRole GetRoleOfUserInTenant();
    }
}
