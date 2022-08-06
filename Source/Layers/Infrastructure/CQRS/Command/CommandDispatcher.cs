using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CQRS.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellation = default) where TCommand : ICommand
        {
            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            return handler.HandleAsync(command, cancellation);
        }
    }
}
