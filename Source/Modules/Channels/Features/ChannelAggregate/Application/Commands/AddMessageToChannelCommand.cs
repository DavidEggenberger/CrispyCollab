using Microsoft.EntityFrameworkCore;
using Shared.Features.CQRS.Command;
using Modules.Channels.Features.Infrastructure.EFCore;

namespace Modules.Channels.Features.ChannelAggregate.Application.Commands
{
    public class AddMessageToChannelCommand : ICommand
    {
        public Guid ChannelId { get; set; }
        public string Text { get; set; }
    }
    public class AddMessageToChannelCommandHandler : ICommandHandler<AddMessageToChannelCommand>
    {
        private readonly ChannelDbContext applicationDbContext;
        public AddMessageToChannelCommandHandler(ChannelDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(AddMessageToChannelCommand command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Include(c => c.Messages).Single(c => c.Id == command.ChannelId);
            channel.AddMessage(new Message { Text = command.Text });
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
