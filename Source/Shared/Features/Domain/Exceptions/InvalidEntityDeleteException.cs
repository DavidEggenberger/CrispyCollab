using Shared.Features.Domain.Exceptions;

namespace Shared.Features.DomainKernel.Exceptions
{
    public class InvalidEntityDeleteException : DomainException
    {
        public InvalidEntityDeleteException(string message) : base(message)
        {

        }
    }
}
