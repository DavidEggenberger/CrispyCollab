using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Exceptions
{
    public class InvalidEntityDeleteException : Exception
    {
        public InvalidEntityDeleteException(string message) : base(message)
        {

        }
    }
}
