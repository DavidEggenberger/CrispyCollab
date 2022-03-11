using Common.Identity.Subscription;
using Microsoft.AspNetCore.Components;

namespace WebWasmClient.Features.ManageTeam.Subscription
{
    public partial class ManageSubscriptionComp
    {
        [Parameter] public SubscriptionDTO Subscription { get; set; }
    }
}
