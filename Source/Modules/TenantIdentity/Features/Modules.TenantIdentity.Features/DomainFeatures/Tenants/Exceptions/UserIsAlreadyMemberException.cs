using Shared.Features.Domain.Exceptions;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Exceptions
{
    public class UserIsAlreadyMemberException : DomainException
    {
        public UserIsAlreadyMemberException() : base("message")
        {
        }
    }
}
