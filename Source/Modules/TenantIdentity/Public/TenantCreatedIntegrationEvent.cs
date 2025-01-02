using Shared.Kernel.BuildingBlocks;

namespace Modules.TenantIdentity.Public
{
    public class TenantCreatedIntegrationEvent : IIntegrationEvent
    {
        public Guid TenantId { get; set; }
    }
}