using Microsoft.AspNetCore.SignalR;
using SharedKernel.Exstensions;

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
