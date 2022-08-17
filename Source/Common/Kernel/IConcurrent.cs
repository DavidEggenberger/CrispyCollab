namespace Common.Kernel
{
    public interface IConcurrent
    {
        byte[] RowVersion { get; set; }
    }
}
