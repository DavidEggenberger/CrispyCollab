using Modules.ChannelModule.Domain;
using Shared.Modules.Layers.Application.CQRS.Command;
using Shared.Modules.Layers.Infrastructure.Interfaces;
using Modules.ChannelModule.Infrastructure.EFCore;
using Shared.Modules.Layers.Infrastructure.CQRS.Command;

namespace Modules.ChannelModule.Layers.Application.Commands
{
    public class DeleteChannelCommand : ICommand
    {
        public Guid ChannelId { get; set; }
    }
    public class DeleteChannelCommandCommandHandler : ICommandHandler<DeleteChannelCommand>
    {
        private readonly ChannelDbContext applicationDbContext;
        private readonly ITenantResolver teamResolver;
        public DeleteChannelCommandCommandHandler(ChannelDbContext applicationDbContext, ITenantResolver teamResolver)
        {
            this.applicationDbContext = applicationDbContext;
            this.teamResolver = teamResolver;
        }
        public async Task HandleAsync(DeleteChannelCommand command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Single(c => c.Id == command.ChannelId);
            applicationDbContext.Channels.Remove(channel);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
            //await aggregatesUINotifierService.UpdateChannels(teamResolver.ResolveTenant());
        }
    }
}
