using Shared.Modules.Layers.Application.CQRS.Command;
using Shared.Modules.Layers.Infrastructure.EFCore;
using Shared.Modules.Layers.Infrastructure.Interfaces;
using Modules.ChannelModule.Domain;
using System.Threading;
using System.Threading.Tasks;
using Shared.Modules.Layers.Infrastructure.CQRS.Command;
using Modules.ChannelModule.Infrastructure.EFCore;

namespace Modules.ChannelModule.Layers.Application.Commands
{
    public class CreateChannelCommand : ICommand
    {
        public string Name { get; set; }
        public string Goal { get; set; }
    }
    public class CreateChannelCommandHandler : ICommandHandler<CreateChannelCommand>
    {
        private readonly ChannelDbContext applicationDbContext;
        private readonly ITenantResolver teamResolver;
        public CreateChannelCommandHandler(ChannelDbContext applicationDbContext, ITenantResolver teamResolver)
        {
            this.applicationDbContext = applicationDbContext;
            this.teamResolver = teamResolver;
        }
        public async Task HandleAsync(CreateChannelCommand command, CancellationToken cancellationToken)
        {
            //applicationDbContext.Channels.Add(new Channel(command.Name, command.Goal, false));
            //await applicationDbContext.SaveChangesAsync(cancellationToken);
            //await aggregatesUINotifierService.UpdateChannels(teamResolver.ResolveTenant());
        }
    }
}
