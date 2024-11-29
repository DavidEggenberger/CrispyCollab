using Shared.Features.Domain.Exceptions;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain.Exceptions
{
    public class MemberNotFoundException : DomainException
    {
        public MemberNotFoundException() : base("message")
        {
        }
    }
}
