using Microsoft.AspNetCore.Components;
using Shared.Kernel.BuildingBlocks.Services.Http;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation;

namespace Shared.Client
{
    public partial class BaseComponent : ComponentBase
    {
        [Inject]
        protected HttpClientService httpClientService { get; set; }

        [Inject]
        protected ValidationService validationService { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }
    }
}
