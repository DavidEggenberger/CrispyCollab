using Microsoft.AspNetCore.Components;
using System;

namespace WebWasmClient.Pages
{
    public partial class Topic
    {
        [Parameter] public Guid TopicId { get; set; }
    }
}
