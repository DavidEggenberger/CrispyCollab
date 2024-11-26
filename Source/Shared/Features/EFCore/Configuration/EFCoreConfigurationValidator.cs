using Microsoft.Extensions.Options;
using Shared.Features.Configuration;

namespace Shared.Features.EFCore.Configuration
{
    internal class EFCoreConfigurationValidator : ConfigurationObjectValidator<EFCoreConfiguration>
    {
        public override ValidateOptionsResult Validate(string name, EFCoreConfiguration efCoreConfiguration)
        {
            if (string.IsNullOrEmpty(efCoreConfiguration.SQLServerConnectionStringDev))
            {
                return ValidateOptionsResult.Fail("");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
