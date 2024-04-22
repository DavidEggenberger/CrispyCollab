using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Messaging.IntegrationEvent
{
    public interface IIntegrationEventDispatcher
    {
        void Raise<TIntegrationEvent>(TIntegrationEvent command, CancellationToken cancellation) where TIntegrationEvent : IIntegrationEvent;

        //Task<TResult> RaiseAndWaitForResultAsync<TIntegrationEvent, TResult>(TIntegrationEvent command, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent<TResult>;
    }
}
