using Domain.Aggregates.ChannelAggregate;
using Infrastructure.CQRS.Command;
using Infrastructure.EFCore;
using Infrastructure.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ChannelAggregate
{
    public class DeleteChannelCommand : ICommand
    {
        public Guid ChannelId { get; set; }
    }
    public class DeleteChannelCommandCommandHandler : ICommandHandler<DeleteChannelCommand>
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ITenantResolver teamResolver;
        public DeleteChannelCommandCommandHandler(ApplicationDbContext applicationDbContext, ITenantResolver teamResolver)
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
