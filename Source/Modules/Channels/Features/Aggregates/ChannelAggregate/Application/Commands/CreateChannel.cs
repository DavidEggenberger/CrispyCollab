using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;
using Shared.Features.MultiTenancy.Services;

namespace Modules.Channels.Features.Aggregates.ChannelAggregate.Application.Commands
{
    public class CreateChannel : ICommand
    {
        public string Name { get; set; }
        public string Goal { get; set; }
    }
    public class CreateChannelCommandHandler : ICommandHandler<CreateChannel>
    {
        private readonly ChannelsDbContext applicationDbContext;
        private readonly ITenantResolver teamResolver;
        public CreateChannelCommandHandler(ChannelsDbContext applicationDbContext, ITenantResolver teamResolver)
        {
            this.applicationDbContext = applicationDbContext;
            this.teamResolver = teamResolver;
        }
        public async Task HandleAsync(CreateChannel command, CancellationToken cancellationToken)
        {
            applicationDbContext.Channels.Add(new Channel(command.Name, command.Goal, false));
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
