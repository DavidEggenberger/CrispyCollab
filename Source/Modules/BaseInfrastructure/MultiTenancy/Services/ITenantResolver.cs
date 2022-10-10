namespace Infrastructure.Interfaces
{
    public interface ITenantResolver
    {
        bool CanResolveTenant();
        Guid ResolveTenantId();
    }
}
