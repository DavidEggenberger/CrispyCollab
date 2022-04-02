using Domain.Aggregates.ChannelAggregate;
using Infrastructure.CQRS.Command;
using Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ChannelAggregate
{
    public class CreateChannelCommand : ICommand
    {
        public string Name { get; set; }
    }
    public class CreateChannelCommandHandler : ICommandHandler<CreateChannelCommand>
    {
        private readonly ApplicationDbContext applicationDbContext;
        public CreateChannelCommandHandler(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(CreateChannelCommand command, CancellationToken cancellationToken)
        {
            applicationDbContext.Channels.Add(new Channel(command.Name, false));
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
