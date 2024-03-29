using Shared.Kernel.BuildingBlocks;

namespace IntegrationEvents
{
    public class TenantCreatedIntegrationEvent : IIntegrationEvent
    {
        public Guid TenantId { get; set; }
    }
}