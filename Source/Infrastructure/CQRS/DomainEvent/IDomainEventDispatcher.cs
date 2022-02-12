using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.DomainEvent
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch<TDomainEvent>(TDomainEvent command, CancellationToken cancellation) where TDomainEvent : IDomainEvent;
    }
}
