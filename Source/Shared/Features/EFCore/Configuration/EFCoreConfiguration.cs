using Shared.Features.Configuration;

namespace Shared.Features.EFCore.Configuration
{
    public class EFCoreConfiguration : ConfigurationObject
    {
        public string SQLServerConnectionStringDev { get; set; }
        public string SQLServerConnectionStringProd { get; set; }
    }
}
