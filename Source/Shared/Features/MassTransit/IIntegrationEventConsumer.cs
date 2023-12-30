using MassTransit;

namespace Shared.Features.MassTransit
{
    public interface IIntegrationEventConsumer<T> : IConsumer<T> where T : class
    {

    }
}
