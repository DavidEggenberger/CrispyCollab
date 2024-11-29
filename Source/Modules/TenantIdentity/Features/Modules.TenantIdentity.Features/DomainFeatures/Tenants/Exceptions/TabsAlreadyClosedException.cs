using Shared.Features.Domain.Exceptions;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Domain.Exceptions
{
    public class TabsAlreadyClosedException : DomainException
    {
        public TabsAlreadyClosedException() : base("message")
        {
        }
    }
}
