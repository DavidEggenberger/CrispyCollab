using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Modules.Layers.Infrastructure.StripeIntegration
{
    public class StripeIntegrationException : Exception
    {
        public StripeIntegrationException(string message) : base(message)
        {

        }
    }
}
