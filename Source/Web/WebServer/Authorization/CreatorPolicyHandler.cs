using Domain.SharedKernel;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace WebServer.Authorization
{
    public class CreatorPolicyHandler : AuthorizationHandler<CreatorPolicyRequirement, Entity>
    {
        private readonly IUserResolver userResolver;
        public CreatorPolicyHandler(IUserResolver userResolver)
        {
            this.userResolver = userResolver;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatorPolicyRequirement requirement, Entity resource)
        {
            if(userResolver.GetIdOfLoggedInUser() == resource.CreatedByUserId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public class CreatorPolicyRequirement : IAuthorizationRequirement
    {

    }
}
