using Domain.Aggregates.ChannelAggregate;
using Infrastructure.CQRS.Command;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ChannelAggregate.Commands
{
    public class DeleteMessageFromChannelCommand : ICommand
    {
        public Guid ChanelId { get; set; }
        public Guid MessageId { get; set; }
    }
    public class DeleteMessageFromChannelCommandHandler : ICommandHandler<DeleteMessageFromChannelCommand>
    {
        private readonly ApplicationDbContext applicationDbContext;
        public DeleteMessageFromChannelCommandHandler(ApplicationDbContext applicationDbContext)
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
