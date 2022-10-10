using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;

namespace Common.Exceptions.Domain
{
    public class InvalidEntityDeleteException : DomainException
    {
        public InvalidEntityDeleteException(string message) : base(message)
        {

        }
    }
}
