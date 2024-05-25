using Shared.Features.Domain;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate
{
    public class TenantSettings : Entity
    {
        public string IconURI { get; set; }
    }
}
