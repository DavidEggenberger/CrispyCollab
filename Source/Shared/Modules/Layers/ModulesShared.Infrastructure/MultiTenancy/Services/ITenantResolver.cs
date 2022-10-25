namespace Shared.Modules.Layers.Infrastructure.Interfaces
{
    public interface ITenantResolver
    {
        bool CanResolveTenant();
        Guid ResolveTenantId();
    }
}
