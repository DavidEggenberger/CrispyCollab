using System;

namespace Modules.IdentityModule.Domain.Exceptions
{
    public class IdentityOperationException : Exception
    {
        public IdentityOperationException(string message = "") : base(message)
        {

        }
    }
}
