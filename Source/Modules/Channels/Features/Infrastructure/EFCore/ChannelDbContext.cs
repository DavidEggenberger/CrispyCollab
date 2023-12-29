using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Features.EFCore;

namespace Modules.Channels.Features.Infrastructure.EFCore
{
    public class ChannelDbContext : BaseDbContext<ChannelDbContext>
    {
        public ChannelDbContext(DbContextOptions<ChannelDbContext> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions, serviceProvider, configuration)
        {

        }

        public DbSet<ChannelAggregate.Channel> Channels { get; set; }
    }
}
