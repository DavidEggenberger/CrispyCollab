using Microsoft.EntityFrameworkCore;
using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Command;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Commands
{
    public class AddMessageToChannel : ICommand
    {
        public Guid ChannelId { get; set; }
        public string Text { get; set; }
    }
    public class AddMessageToChannelCommandHandler : ICommandHandler<AddMessageToChannel>
    {
        private readonly ChannelsDbContext applicationDbContext;
        public AddMessageToChannelCommandHandler(ChannelsDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(AddMessageToChannel command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Include(c => c.Messages).Single(c => c.Id == command.ChannelId);
            channel.AddMessage(new Message { Text = command.Text });
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
