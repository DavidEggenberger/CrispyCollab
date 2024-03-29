using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;

namespace Modules.Channels.Features.DomainFeatures.ChannelAggregate.Application.Commands
{
    public class ChangeChannelName : ICommand
    {
        public Guid ChannelId { get; set; }
        public string NewName { get; set; }
    }

    public class ChangeChannelNameCommandHandler : ICommandHandler<ChangeChannelName>
    {
        private readonly ChannelsDbContext applicationDbContext;
        public ChangeChannelNameCommandHandler(ChannelsDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(ChangeChannelName command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Single(c => c.Id == command.ChannelId);
            channel.Name = command.NewName;
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
