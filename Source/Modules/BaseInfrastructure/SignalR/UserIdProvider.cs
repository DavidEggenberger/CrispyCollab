using Microsoft.AspNetCore.SignalR;
using Common.Exstensions;

namespace Infrastructure.SignalR
{
    public class UserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User.GetUserId<string>();
        }
    }
}
