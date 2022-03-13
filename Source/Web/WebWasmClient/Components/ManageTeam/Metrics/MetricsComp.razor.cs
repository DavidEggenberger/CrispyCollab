using Common.Identity.Team.AdminManagement;
using Microsoft.AspNetCore.Components;

namespace WebWasmClient.Components.ManageTeam.Metrics
{
    public partial class MetricsComp
    {
        [Parameter] public TeamMetricsDTO TeamMetrics { get; set; }
    }
}
