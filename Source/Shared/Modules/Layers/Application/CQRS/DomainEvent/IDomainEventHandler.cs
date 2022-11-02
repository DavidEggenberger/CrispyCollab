using Shared.Modules.Layers.Domain.Interfaces;
using Shared.SharedKernel.Interfaces;

namespace Shared.Modules.Layers.Application.CQRS.DomainEvent
{
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Task HandleAsync(TDomainEvent query, CancellationToken cancellation);
    }
}
