using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WebServer.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User.FindFirst(ClaimTypes.Sid)?.Value;
        }
    }
}
