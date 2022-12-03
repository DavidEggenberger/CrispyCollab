namespace ModulesShared.Infrastructure.EventBus
{
    public interface IIntegrationEventPublisher
    {
        Task PublishIntegrationEventAsync<T>(T integrationEvent, CancellationToken cancellationToken) where T : class;
    }
}
