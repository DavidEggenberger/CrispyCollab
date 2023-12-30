using Shared.Features.DomainKernel;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate
{
    public class TenantSettings : Entity
    {
        public string IconURI { get; set; }
    }
}
