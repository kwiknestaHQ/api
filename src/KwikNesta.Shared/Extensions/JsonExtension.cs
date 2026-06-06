using System.Text.Json;

namespace KwikNesta.Shared.Extensions
{
    public static class JsonExtension
    {
        public static T DeserializeObject<T>(this JsonElement root) where T : class
        {
            return root.Deserialize<T>() ?? 
                throw new InvalidOperationException($"Failed to deserialize {typeof(T).Name}");
        }
    }
}