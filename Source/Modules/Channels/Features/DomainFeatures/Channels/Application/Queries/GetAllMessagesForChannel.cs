using Modules.Channels.Features.Infrastructure.EFCore;
using Modules.Subscriptions.Features;
using Shared.Features.Messaging.Query;
using Shared.Features.Server;

namespace Modules.Channels.Features.DomainFeatures.Channels.Application.Queries
{
    public class AllMessagesForChannel : Query<List<Channel>>
    {

    }
    public class AllMessagesForChannelQueryHandler : ServerExecutionBase<ChannelsModule>, IQueryHandler<GetAllChannels, List<Channel>>
    {
        public AllMessagesForChannelQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<List<Channel>> HandleAsync(GetAllChannels query, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
