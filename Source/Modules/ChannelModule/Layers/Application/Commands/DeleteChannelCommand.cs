using Modules.ChannelModule.Domain;
using Infrastructure.CQRS.Command;
using Infrastructure.EFCore;
using Infrastructure.Interfaces;
using Modules.ChannelModule.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Modules.ChannelModule.Infrastructure.EFCore;

namespace Application.ChannelAggregate
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
