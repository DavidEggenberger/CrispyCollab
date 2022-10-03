namespace Common.Kernel
{
    public interface ITenantIdentifiable
    {
        Guid TenantId { get; set; }
    }
}
