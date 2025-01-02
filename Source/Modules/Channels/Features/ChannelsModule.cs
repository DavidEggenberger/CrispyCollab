using Modules.Channels.Features.Infrastructure.EFCore;
using Shared.Features.Modules;
using System.Reflection;

namespace Modules.Channels.Features
{
    public class ChannelsModule : IModule
    {
        public Assembly FeaturesAssembly => typeof(ChannelsModule).Assembly;
        public ChannelsDbContext ChannelsDbContext { get; set; }

        public ChannelsModule(ChannelsDbContext dbContext)
        {
            ChannelsDbContext = dbContext;
        }
    }
}
