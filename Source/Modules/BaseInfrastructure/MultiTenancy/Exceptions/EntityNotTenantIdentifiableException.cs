using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MultiTenancy.Exceptions
{
    public class EntityNotTenantIdentifiableException : Exception
    {
        public EntityNotTenantIdentifiableException(string message) : base(message)
        {

        }
    }
}
