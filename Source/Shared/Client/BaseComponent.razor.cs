using Microsoft.AspNetCore.Components;
using Shared.Kernel.BuildingBlocks.Services.Http;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
