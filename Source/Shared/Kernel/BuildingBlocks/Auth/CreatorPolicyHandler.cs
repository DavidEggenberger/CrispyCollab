using Microsoft.AspNetCore.Authorization;
using Shared.Kernel.Extensions.ClaimsPrincipal;
using Shared.SharedKernel.Interfaces;

namespace Shared.Kernel.BuildingBlocks.Authorization
{
    public class CreatorPolicyHandler : AuthorizationHandler<CreatorPolicyRequirement, IAuditable>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatorPolicyRequirement requirement, IAuditable resource)
        {
            if (context.User.GetUserId<Guid>() == resource.UserId)
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
