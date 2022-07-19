using WebShared.Identity.Subscription;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebWasmClient.Services;

namespace WebWasmClient.Components.ManageTeam.Subscription
{
    public partial class ManageSubscriptionComp
    {
        [Parameter] public SubscriptionDTO CurrentSubscription { get; set; }

        private List<SubscriptionPlanDTO> subscriptionPlans = new List<SubscriptionPlanDTO>();
        protected override async Task OnInitializedAsync()
        {
            subscriptionPlans = await HttpClientService.GetFromAPIAsync<List<SubscriptionPlanDTO>>("/subscriptionplan/all");
        }
    }
}
