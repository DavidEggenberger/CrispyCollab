using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.Exstensions;

namespace WebServer.Services
{
    public class UserResolver : IUserResolver
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public Guid GetIdOfLoggedInUser()
        {
            return httpContextAccessor.HttpContext.User.GetUserIdAsGuid();
        }
    }
}
