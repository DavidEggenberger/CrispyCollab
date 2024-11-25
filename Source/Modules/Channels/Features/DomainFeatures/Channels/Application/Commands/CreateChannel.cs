using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Command;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Commands
{
    public class CreateChannel : Command
    {
        public string Name { get; set; }
        public string Goal { get; set; }
    }
    public class CreateChannelCommandHandler : ICommandHandler<CreateChannel>
    {
        private readonly ChannelsDbContext applicationDbContext;
        public CreateChannelCommandHandler(ChannelsDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(CreateChannel command, CancellationToken cancellationToken)
        {
            applicationDbContext.Channels.Add(new Channel(command.Name, command.Goal, false));
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
