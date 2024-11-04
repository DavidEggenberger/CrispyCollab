using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modules.Channels.Features.DomainFeatures.Channels;
using Shared.Features.EFCore;

namespace Modules.Channels.Features.Infrastructure.EFCore
{
    public class ChannelsDbContext : BaseDbContext<ChannelsDbContext>
    {
        public ChannelsDbContext(DbContextOptions<ChannelsDbContext> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, "Channels", dbContextOptions)
        {

        }

        public DbSet<Channel> Channels { get; set; }
    }
}
