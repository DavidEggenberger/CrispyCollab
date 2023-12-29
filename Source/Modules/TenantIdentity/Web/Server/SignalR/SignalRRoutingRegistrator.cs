using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Modules.TenantIdentity.Web.Server.SignalR
{
    public static class SignalRRoutingRegistrator
    {
        public static HubEndpointConventionBuilder MapSignalR(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            return null;
            //return endpointRouteBuilder.MapHub<NotificationHub>(SignalRConstants.NotificationEndpoint);
        }
    }
}
