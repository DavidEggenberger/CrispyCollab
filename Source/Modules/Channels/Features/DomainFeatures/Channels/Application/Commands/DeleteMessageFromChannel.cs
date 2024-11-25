using Microsoft.EntityFrameworkCore;
using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Command;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Commands
{
    public class DeleteMessageFromChannel : Command
    {
        public Guid ChanelId { get; set; }
        public Guid MessageId { get; set; }
    }
    public class DeleteMessageFromChannelCommandHandler : ICommandHandler<DeleteMessageFromChannel>
    {
        private readonly ChannelsDbContext applicationDbContext;
        public DeleteMessageFromChannelCommandHandler(ChannelsDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(DeleteMessageFromChannel command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Include(c => c.Messages).Single(c => c.Id == command.ChanelId);
            channel.RemoveMessage(null);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
