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

namespace Application.ChannelAggregate
{
    public class UpdateChannelCommand : ICommand
    {
        public Guid ChannelId { get; set; }
        public string NewName { get; set; }
    }
    public class UpdateChannelCommandHandler : ICommandHandler<UpdateChannelCommand>
    {
        private readonly ApplicationDbContext applicationDbContext;
        public UpdateChannelCommandHandler(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task HandleAsync(UpdateChannelCommand command, CancellationToken cancellationToken)
        {
            Channel channel = applicationDbContext.Channels.Single(c => c.Id == command.ChannelId);
            channel.Name = command.NewName;
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
