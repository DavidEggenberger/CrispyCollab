using Common.Kernel;

namespace Domain.Kernel
{
    public class ValueObject :  IAuditable, IIdentifiable, ITenantIdentifiable
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
