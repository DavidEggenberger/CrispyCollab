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
        public HttpClientService HttpClientService { get; set; }

        [Inject]
        public ValidationService ValidationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
    }
}
