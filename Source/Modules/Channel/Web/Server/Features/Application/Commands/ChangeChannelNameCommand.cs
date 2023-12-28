using ChannelModule.Server.Features.Domain;
using Modules.ChannelModule.Infrastructure.EFCore;
using Shared.Infrastructure.CQRS.Command;

namespace ChannelModule.Server.Features.Application.Commands
{
    public class ChangeChannelNameCommand : ICommand
    {
        public Guid ChannelId { get; set; }
        public string NewName { get; set; }
    }
    public class ChangeChannelNameCommandHandler : ICommandHandler<ChangeChannelNameCommand>
    {
        private readonly ChannelDbContext applicationDbContext;
        public ChangeChannelNameCommandHandler(ChannelDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(ChangeChannelNameCommand command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Single(c => c.Id == command.ChannelId);
            channel.Name = command.NewName;
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
