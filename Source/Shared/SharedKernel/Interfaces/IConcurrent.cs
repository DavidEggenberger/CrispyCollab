namespace SharedKernel.Kernel
{
    public interface IConcurrent
    {
        byte[] RowVersion { get; set; }
    }
}
