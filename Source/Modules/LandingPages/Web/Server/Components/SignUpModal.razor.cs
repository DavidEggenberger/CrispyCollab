using Microsoft.AspNetCore.Components;

namespace Modules.LandingPages.Server.Components
{
    public partial class SignUpModal
    {
        [Parameter]
        public EventCallback CancelRequestedCallback { get; set; }

        private bool switchToSignIn;

        public async Task CancelRequestedAsync()
        {
            if (CancelRequestedCallback.HasDelegate)
            {
                await CancelRequestedCallback.InvokeAsync(this);
            }
        }
    }
}
