using Common.Identity.Team.AdminManagement;
using Microsoft.AspNetCore.Components;

namespace WebWasmClient.Features.ManageTeam.Metrics
{
    public partial class MetricsComp
    {
        [Parameter] public TeamMetricsDTO TeamMetrics { get; set; }
    }
}
