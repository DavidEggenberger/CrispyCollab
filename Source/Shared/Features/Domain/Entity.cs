using Shared.Features.DomainKernel.Exceptions;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.DomainKernel;
using Shared.Kernel.Errors.Exceptions;
using Shared.SharedKernel.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Features.Domain
{
    public abstract class Entity : IAuditable, IIdentifiable, IConcurrent
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual Guid TenantId { get; set; }

        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset LastUpdatedAt { get; set; }

        [NotMapped]
        public IExecutionContext ExecutionContext { get; set; }

        public void ThrowIfCallerIsNotInRole(TenantRole role)
        {
            if (ExecutionContext.TenantRole != role)
            {
                throw new UnAuthorizedException();
            }
        }
    }
}
