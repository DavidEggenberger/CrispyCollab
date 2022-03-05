using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Types
{
    public class IdentityOperationException : Exception
    {
        public IdentityOperationException(string message) : base(message)
        {

        }
    }
}
