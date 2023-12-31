using Shared.SharedKernel.Interfaces;

namespace Shared.Features.DomainKernel
{
    public class AggregateRoot : Entity, ITenantIdentifiable
    {
        public Guid TenantId { get; set; }
    }
}
