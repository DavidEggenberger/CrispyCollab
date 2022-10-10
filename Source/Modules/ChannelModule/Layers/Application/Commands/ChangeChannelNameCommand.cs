using Modules.ChannelModule.Domain;
using Infrastructure.CQRS.Command;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Modules.ChannelModule.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Modules.ChannelModule.Infrastructure.EFCore;

namespace Application.ChannelAggregate
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
