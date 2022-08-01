namespace Shared.Kernel
{
    public interface IAuditable
    {
        Guid CreatedByUserId { get; set; }
    }
}
