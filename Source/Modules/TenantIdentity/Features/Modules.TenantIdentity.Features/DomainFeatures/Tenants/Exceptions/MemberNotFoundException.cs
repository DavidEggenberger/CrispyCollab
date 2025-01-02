using Shared.Features.Domain.Exceptions;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Exceptions
{
    public class MemberNotFoundException : DomainException
    {
        public MemberNotFoundException() : base("message")
        {
        }
    }
}
