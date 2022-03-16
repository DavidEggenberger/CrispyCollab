using Domain.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CQRS.DomainEvent
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public Task DispatchAsync<TDomainEvent>(TDomainEvent command, CancellationToken cancellation = default) where TDomainEvent : IDomainEvent
        {
            var handler = serviceProvider.GetRequiredService<IDomainEventHandler<TDomainEvent>>();
            return handler.HandleAsync(command, cancellation);
        }

        public Task<TDomainEventResponse> DispatchAsync<TDomainEventResponse>(IDomainEvent<TDomainEventResponse> domainEvent, CancellationToken cancellation = default)
        {
            var handler = serviceProvider.GetRequiredService<IDomainEventHandler<IDomainEvent<TDomainEventResponse>, TDomainEventResponse>>();
            return handler.HandleAsync(domainEvent, cancellation);
        }
    }
}
