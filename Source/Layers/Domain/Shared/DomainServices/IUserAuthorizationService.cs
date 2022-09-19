using Domain.Aggregates.TenantAggregate.Enums;

namespace Domain.Shared.DomainServices
{
    public interface IUserAuthorizationService
    {
        void ThrowIfUserIsNotInRole(Role role);
        Role GetRoleOfUserInTenant(); 
    }
}
