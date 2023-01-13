using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modules.ChannelModule.Domain;
using Shared.Infrastructure.EFCore;

namespace Modules.ChannelModule.Infrastructure.EFCore
{
    public class ChannelDbContext : BaseDbContext<ChannelDbContext>
    {
        public ChannelDbContext(DbContextOptions<ChannelDbContext> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions, serviceProvider, configuration)
        {

        }

        public DbSet<Channel> Channels { get; set; }
    }
}
