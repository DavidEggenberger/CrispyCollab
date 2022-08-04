using Domain.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace Infrastructure.CQRS.DomainEvent
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task DispatchAsync<TDomainEvent>(TDomainEvent command, CancellationToken cancellation = default) where TDomainEvent : IDomainEvent
        {
            var eventHandlers = serviceProvider.GetServices<IDomainEventHandler<TDomainEvent>>();
            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.HandleAsync(command, cancellation);
            }
        }
    }
}
