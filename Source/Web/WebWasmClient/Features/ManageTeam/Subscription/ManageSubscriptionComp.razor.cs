using Common.Identity.Subscription;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebWasmClient.Features.ManageTeam.Subscription
{
    public partial class ManageSubscriptionComp
    {
        [Parameter] public SubscriptionDTO Subscription { get; set; }

        private List<SubscriptionPlanDTO> subscriptionPlans;
        protected override async Task OnInitializedAsync()
        {
            subscriptionPlans = await HttpClientService.GetFromAPIAsync<List<SubscriptionPlanDTO>>("/subscriptionplan/all");
        }
    }
}
