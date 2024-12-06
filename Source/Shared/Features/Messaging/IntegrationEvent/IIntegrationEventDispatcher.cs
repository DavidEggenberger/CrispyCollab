using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Messaging.IntegrationEvent
{
    public interface IIntegrationEventDispatcher
    {
        void Raise<TIntegrationEvent>(TIntegrationEvent command, CancellationToken cancellation) where TIntegrationEvent : IIntegrationEvent;

        Task RaiseAndWaitForCompletionAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent;
    }
}
