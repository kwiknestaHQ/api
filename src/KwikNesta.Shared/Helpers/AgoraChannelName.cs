using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.Helpers
{
    public class AgoraChannelName
    {
        public static string Generate(string env, EAgoraModule module, string entityId)
        {
            if (string.IsNullOrWhiteSpace(env) || string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentNullException("Environment Name and EntityId are required");
            }

            return $"{env.ToLower()}:" + 
                   $"{module.ToString().ToLower()}:" + 
                   $"{entityId}";
        }

        public static AgoraChannelContext Decode(string channelName)
        {
            if (string.IsNullOrWhiteSpace(channelName))
            {
                throw new ArgumentNullException("Channel Name is required.");
            }

            var parts = channelName.Split(':');
            if(parts.Length != 3)
            {
                throw new FormatException("Invalid channel name format.");
            }

            return new AgoraChannelContext
            {
                Environment = parts[0].CapitalizeEachWord(),
                Module = Enum.Parse<EAgoraModule>(parts[1].CapitalizeEachWord()),
                EntityId = parts[2]
            };
        }
    }
}