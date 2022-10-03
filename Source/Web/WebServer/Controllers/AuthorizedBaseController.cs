using Application.TenantAggregate.Queries;
using Domain.Aggregates.TenantAggregate;
using Infrastructure.CQRS.Query;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Modules.TenantModule.Domain;

namespace WebServer.Controllers
{
    public class AuthorizedBaseController : Controller
    {
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IQueryDispatcher queryDispatcher;
        public AuthorizedBaseController()
        {
            applicationUserManager = HttpContext.RequestServices.GetRequiredService<ApplicationUserManager>();
            queryDispatcher = HttpContext.RequestServices.GetRequiredService<IQueryDispatcher>();
        }

        protected ApplicationUser ApplicationUser { get; set; }
        protected Tenant Tenant { get; set; }
        public RoleConstants MyProperty { get; set; }

        [NonAction]
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            ApplicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(context.HttpContext.User);
            var tenantByIdQuery = new GetTenantByQuery() { TenantId = ApplicationUser.SelectedTenantId };
            Tenant = await queryDispatcher.DispatchAsync<GetTenantByQuery, Tenant>(tenantByIdQuery);
        }
    }
}
