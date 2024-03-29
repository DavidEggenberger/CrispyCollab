using WebShared.Identity.Team.AdminManagement;
using Microsoft.AspNetCore.Components;
using Shared.Client;

namespace Modules.TenantIdentity.Web.Client.Components
{
    public partial class TenantMetricsComponentBase : BaseComponent
    {
        [Parameter] public TeamMetricsDTO TeamMetrics { get; set; }
    }
}
