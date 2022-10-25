using Modules.ChannelModule.Domain;
using Shared.Modules.Layers.Application.CQRS.Command;
using Shared.Modules.Layers.Infrastructure.CQRS.Command;
using Modules.ChannelModule.Infrastructure.EFCore;

namespace Modules.ChannelModule.Layers.Application.Commands
{
    public class ChangeChannelNameCommand : ICommand
    {
        public Guid ChannelId { get; set; }
        public string NewName { get; set; }
    }
    public class ChangeChannelNameCommandHandler : ICommandHandler<ChangeChannelNameCommand>
    {
        private readonly ChannelDbContext applicationDbContext;
        public ChangeChannelNameCommandHandler(ChannelDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(ChangeChannelNameCommand command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Single(c => c.Id == command.ChannelId);
            channel.Name = command.NewName;
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
