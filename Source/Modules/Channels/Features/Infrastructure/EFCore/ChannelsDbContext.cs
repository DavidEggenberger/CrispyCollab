using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Features.EFCore;

namespace Modules.Channels.Features.Infrastructure.EFCore
{
    public class ChannelsDbContext : BaseDbContext<ChannelsDbContext>
    {
        public ChannelsDbContext(DbContextOptions<ChannelsDbContext> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions, serviceProvider, configuration)
        {

        }

        public DbSet<ChannelAggregate.Channel> Channels { get; set; }
    }
}
