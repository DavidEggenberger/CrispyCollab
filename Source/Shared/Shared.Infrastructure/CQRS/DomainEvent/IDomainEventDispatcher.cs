using Shared.Features.DomainKernel.Interfaces;

namespace Shared.Features.CQRS.Features.DomainKernelEvent
{
    public interface IDomainEventDispatcher
    {
        Task RaiseAsync<TDomainEvent>(TDomainEvent command, CancellationToken cancellation) where TDomainEvent : IDomainEvent;
    }
}
