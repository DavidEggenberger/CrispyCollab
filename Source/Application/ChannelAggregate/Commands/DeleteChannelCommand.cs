using Domain.Aggregates.ChannelAggregate;
using Infrastructure.CQRS.Command;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ChannelAggregate
{
    public class DeleteChannelCommand : ICommand
    {
        public Channel Channel { get; set; }
    }
    public class DeleteChannelCommandCommandHandler : ICommandHandler<DeleteChannelCommand>
    {
        private readonly ApplicationDbContext applicationDbContext;
        public DeleteChannelCommandCommandHandler(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(DeleteChannelCommand command, CancellationToken cancellationToken)
        {
            applicationDbContext.Channels.Add(command.Channel);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
