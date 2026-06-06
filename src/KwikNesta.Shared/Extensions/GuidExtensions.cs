namespace KwikNesta.Shared.Extensions
{
    public static class GuidExtensions
    {
        public static uint ToUId(this Guid guid)
        {
            return (uint)Math.Abs(guid.GetHashCode());
        }
    }
}