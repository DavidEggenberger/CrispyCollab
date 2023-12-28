using Microsoft.EntityFrameworkCore;
using Modules.ChannelModule.Infrastructure.EFCore;
using Shared.Infrastructure.CQRS.Command;
using ChannelModule.Server.Features.Domain;

namespace ChannelModule.Server.Features.Application.Commands
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
