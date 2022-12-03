using Shared.Modules.Layers.Infrastructure.CQRS.Command;

namespace Shared.Modules.Layers.Application.CQRS.Command
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellation = default) where TCommand : ICommand;
        Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command, CancellationToken cancellation = default) where TCommand : ICommand;
    }
}
