using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;
using Shared.Features.MultiTenancy.Services;

namespace Modules.Channels.Features.ChannelAggregate.Application.Commands
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
