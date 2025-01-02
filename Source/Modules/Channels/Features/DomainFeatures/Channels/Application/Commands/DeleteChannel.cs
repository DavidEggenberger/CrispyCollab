using Shared.Features.Messaging.Command;
using Shared.Features.Server;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Commands
{
    public class DeleteChannel : Command
    {
        public Guid ChannelId { get; set; }
    }
    public class DeleteChannelCommandCommandHandler : ServerExecutionBase<ChannelsModule>, ICommandHandler<DeleteChannel>
    {
        public DeleteChannelCommandCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task HandleAsync(DeleteChannel command, CancellationToken cancellationToken)
        {
            Channel channel = module.ChannelsDbContext.Channels.Single(c => c.Id == command.ChannelId);
            module.ChannelsDbContext.Channels.Remove(channel);
            await module.ChannelsDbContext.SaveChangesAsync(cancellationToken);
            //await aggregatesUINotifierService.UpdateChannels(teamResolver.ResolveTenant());
        }
    }
}
