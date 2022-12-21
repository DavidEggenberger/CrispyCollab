using Shared.Modules.Layers.Domain.Interfaces;
using Shared.SharedKernel.Interfaces;

namespace Shared.Modules.Layers.Application.CQRS.DomainEvent
{
    public interface IDomainEventDispatcher
    {
        Task RaiseAsync<TDomainEvent>(TDomainEvent command, CancellationToken cancellation) where TDomainEvent : IDomainEvent;
    }
}
