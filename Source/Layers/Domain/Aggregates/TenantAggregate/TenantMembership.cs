namespace Domain.Aggregates.TenantAggregate
{
    public class TenantMembership
    {
        public Guid UserId { get; set; }
        public Tenant Tenant { get; set; }
        public TenantRole Role { get; set; }
    }
}
