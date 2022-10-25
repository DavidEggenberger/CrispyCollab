namespace Shared.SharedKernel.Interfaces
{
    public interface IConcurrent
    {
        byte[] RowVersion { get; set; }
    }
}
