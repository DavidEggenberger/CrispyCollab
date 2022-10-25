using Shared.Modules.Layers.Application.CQRS.Command;
using Microsoft.EntityFrameworkCore;
using Shared.Modules.Layers.Infrastructure.CQRS.Command;
using Modules.ChannelModule.Infrastructure.EFCore;
using Modules.ChannelModule.Domain;

namespace Modules.ChannelModule.Layers.Application.Commands
{
    public class DeleteMessageFromChannelCommand : ICommand
    {
        public Guid ChanelId { get; set; }
        public Guid MessageId { get; set; }
    }
    public class DeleteMessageFromChannelCommandHandler : ICommandHandler<DeleteMessageFromChannelCommand>
    {
        private readonly ChannelDbContext applicationDbContext;
        public DeleteMessageFromChannelCommandHandler(ChannelDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(DeleteMessageFromChannelCommand command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Include(c => c.Messages).Single(c => c.Id == command.ChanelId);
            channel.RemoveMessage(null);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
