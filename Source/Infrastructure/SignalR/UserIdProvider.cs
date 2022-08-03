using Microsoft.AspNetCore.SignalR;
using Shared.Exstensions;

namespace Infrastructure.SignalR
{
    public class UserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User.GetUserIdAsString();
        }
    }
}
