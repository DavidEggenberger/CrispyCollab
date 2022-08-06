namespace Infrastructure.CQRS.Command
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellation) where TCommand : ICommand;
    }
}
