using Domain.Aggregates.ChannelAggregate;
using Infrastructure.CQRS.Command;
using Infrastructure.EFCore;
using Infrastructure.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ChannelAggregate
{
    public class CreateChannelCommand : ICommand
    {
        public string Name { get; set; }
        public string Goal { get; set; }
    }
    public class CreateChannelCommandHandler : ICommandHandler<CreateChannelCommand>
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ITenantResolver teamResolver;
        public CreateChannelCommandHandler(ApplicationDbContext applicationDbContext, ITenantResolver teamResolver)
        {
            this.applicationDbContext = applicationDbContext;
            this.teamResolver = teamResolver;
        }
        public async Task HandleAsync(CreateChannelCommand command, CancellationToken cancellationToken)
        {
            applicationDbContext.Channels.Add(new Channel(command.Name, command.Goal, false));
            await applicationDbContext.SaveChangesAsync(cancellationToken);
            //await aggregatesUINotifierService.UpdateChannels(teamResolver.ResolveTenant());
        }
    }
}
