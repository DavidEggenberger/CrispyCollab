namespace SharedKernel.Kernel
{
    public interface ITenantIdentifiable
    {
        Guid TenantId { get; set; }
    }
}
