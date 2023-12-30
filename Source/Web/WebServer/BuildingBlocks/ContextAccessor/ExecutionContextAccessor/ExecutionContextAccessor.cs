using Microsoft.AspNetCore.Http;
using Shared.Features.DomainKernel;
using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Kernel.Extensions.ClaimsPrincipal;
using System;

namespace Web.Server.BuildingBlocks.ContextAccessor.ExecutionContextAccessor
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private HttpContext capturedHttpContext;

        public void CaptureHttpContext(HttpContext httpContext)
        {
            capturedHttpContext = httpContext;
        }

        public Guid UserId
        {
            get
            {
                if (capturedHttpContext.User.Identity.IsAuthenticated is false)
                {
                    return Guid.Empty;
                }
                return capturedHttpContext.User.GetUserId<Guid>();
            }
        }

        public SubscriptionPlanType SubscriptionPlan
        {
            get
            {
                if (capturedHttpContext.User.Identity.IsAuthenticated is false)
                {
                    return SubscriptionPlanType.Free;
                }

                return capturedHttpContext.User.GetSubscriptionPlanType();
            }
        }
    }
}
