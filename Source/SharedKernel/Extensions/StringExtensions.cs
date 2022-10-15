namespace SharedKernel.Exstensions
{
    public static class StringExtensions
    {
        public static Guid ToGuid(this string stringGuid)
        {
            return new Guid(stringGuid);
        }
    }
}
