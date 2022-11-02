using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ModulesShared.Infrastructure.EventBus
{
    public static class EventBusRegistrator
    {
        public static IServiceCollection RegisterEventBus(this IServiceCollection serviceCollection, Type consumerAssemblyMarkerType)
        {
            serviceCollection.AddMassTransit(options =>
            {
                options.AddConsumersFromNamespaceContaining(consumerAssemblyMarkerType);
                options.UsingRabbitMq((a, e) =>
                {
                    
                });
            });

            serviceCollection.AddScoped<IIntegrationEventPublisher, IntegrationEventMassTransitProducer>();

            return serviceCollection;
        }
    }
}
