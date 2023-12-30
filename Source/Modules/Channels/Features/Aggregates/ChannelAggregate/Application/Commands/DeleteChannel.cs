using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;

namespace Modules.Channels.Features.Aggregates.ChannelAggregate.Application.Commands
{
    public class DeleteChannel : ICommand
    {
        public Guid ChannelId { get; set; }
    }
    public class DeleteChannelCommandCommandHandler : ICommandHandler<DeleteChannel>
    {
        private readonly ChannelsDbContext applicationDbContext;
        public DeleteChannelCommandCommandHandler(ChannelsDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(DeleteChannel command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Single(c => c.Id == command.ChannelId);
            applicationDbContext.Channels.Remove(channel);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
            //await aggregatesUINotifierService.UpdateChannels(teamResolver.ResolveTenant());
        }
    }
}
