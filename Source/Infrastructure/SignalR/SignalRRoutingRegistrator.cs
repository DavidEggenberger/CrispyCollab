using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Shared.Constants;

namespace Infrastructure.SignalR
{
    public static class SignalRRoutingRegistrator
    {
        public static HubEndpointConventionBuilder MapSignalR(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            return endpointRouteBuilder.MapHub<NotificationHub>(SignalRConstants.NotificationEndpoint);
        }
    }
}
