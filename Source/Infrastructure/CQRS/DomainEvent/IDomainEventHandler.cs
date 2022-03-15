using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.DomainEvent
{
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Task HandleAsync(TDomainEvent query, CancellationToken cancellation);
    }

    public interface IDomainEventHandler<in TDomainEvent, TDomainEventResponse> where TDomainEvent : IDomainEvent<TDomainEventResponse>
    {
        Task<TDomainEventResponse> HandleAsync(IDomainEvent<TDomainEventResponse> query, CancellationToken cancellation);
    }
}
