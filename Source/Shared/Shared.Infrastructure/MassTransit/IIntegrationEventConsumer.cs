using MassTransit;

namespace ModulesShared.Infrastructure.EventBus
{
    public interface IIntegrationEventConsumer<T> : IConsumer<T> where T : class
    {

    }
}
