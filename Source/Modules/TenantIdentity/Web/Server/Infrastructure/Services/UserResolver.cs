using Shared.Modules.Layers.Features.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.SharedKernel.Exstensions;

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
            return httpContextAccessor.HttpContext.User.GetUserId<Guid>();
        }
    }
}
