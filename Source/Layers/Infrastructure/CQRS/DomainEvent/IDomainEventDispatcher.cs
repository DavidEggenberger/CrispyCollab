using Common.Kernel;

namespace Infrastructure.CQRS.DomainEvent
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync<TDomainEvent>(TDomainEvent command, CancellationToken cancellation) where TDomainEvent : IDomainEvent;
    }
}
