using Microsoft.AspNetCore.Components;
using Modules.Shared.Subscriptions.Shared.DTOs;
using Shared.Client;

namespace Modules.TenantIdentity.Web.Client.Components
{
    public partial class ManageSubscriptionComponentBase : BaseComponent
    {
        [Parameter] public SubscriptionDTO CurrentSubscription { get; set; }

        protected List<SubscriptionPlanDTO> subscriptionPlans = new List<SubscriptionPlanDTO>();
        protected override async Task OnInitializedAsync()
        {
            subscriptionPlans = await httpClientService.GetFromAPIAsync<List<SubscriptionPlanDTO>>("/subscriptionplan/all");
        }
    }
}
