using Domain.Aggregates.ChannelAggregate;
using Domain.Aggregates.MessagingAggregate;
using Domain.Interfaces;
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
    public class AddMessageToChannelCommand : ICommand
    {
        public Guid ChannelId { get; set; }
        public string Text { get; set; }
    }
    public class AddMessageToChannelCommandHandler : ICommandHandler<AddMessageToChannelCommand>
    {
        private readonly IAggregatesUINotifierService aggregatesUINotifierService;
        private readonly ApplicationDbContext applicationDbContext;
        public AddMessageToChannelCommandHandler(ApplicationDbContext applicationDbContext, IAggregatesUINotifierService aggregatesUINotifierService)
        {
            this.applicationDbContext = applicationDbContext;
            this.aggregatesUINotifierService = aggregatesUINotifierService;
        }
        public async Task HandleAsync(AddMessageToChannelCommand command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Include(c => c.Messages).Single(c => c.Id == command.ChannelId);
            channel.AddMessage(new Message { Text = command.Text });
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
