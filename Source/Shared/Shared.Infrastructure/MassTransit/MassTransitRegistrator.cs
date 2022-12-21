using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ModulesShared.Infrastructure.EventBus
{
    public static class MassTransitRegistrator
    {
        public static IServiceCollection RegisterEventBus(this IServiceCollection serviceCollection, Type consumerAssemblyMarkerType)
        {
            serviceCollection.AddMassTransit(options =>
            {
                options.AddConsumersFromNamespaceContaining(consumerAssemblyMarkerType);
                options.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Lazy = true;

                    
                });
            });

            serviceCollection.AddScoped<IIntegrationEventPublisher, IntegrationEventMassTransitProducer>();

            return serviceCollection;
        }
    }
}
