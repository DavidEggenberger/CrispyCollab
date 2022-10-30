using Modules.ChannelModule.Domain;
using Shared.Modules.Layers.Application.CQRS.Command;
using Microsoft.EntityFrameworkCore;
using Shared.Modules.Layers.Infrastructure.CQRS.Command;
using Modules.ChannelModule.Infrastructure.EFCore;

namespace Modules.ChannelModule.Layers.Application.Commands
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
