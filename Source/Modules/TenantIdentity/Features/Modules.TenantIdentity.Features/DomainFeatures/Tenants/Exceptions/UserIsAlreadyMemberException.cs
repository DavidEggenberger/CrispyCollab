using Shared.Features.Domain.Exceptions;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain.Exceptions
{
    public class UserIsAlreadyMemberException : DomainException
    {
        public UserIsAlreadyMemberException() : base("message")
        {
        }
    }
}
