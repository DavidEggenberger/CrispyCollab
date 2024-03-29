using Shared.Features.Modules.Configuration;

namespace Shared.Features.EFCore.Configuration
{
    public class EFCoreConfiguration : IModuleConfiguration
    {
        public string SQLServerConnectionString { get; set; }
    }
}
