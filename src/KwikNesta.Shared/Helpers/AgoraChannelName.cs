using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Infra;
using KwikNesta.Shared.ServiceDTOs.Infra;

namespace KwikNesta.Shared.Helpers
{
    public class AgoraChannelName
    {
        public static string Generate(string env, EAgoraModule module, string entityId, string secretKey)
        {
            if (string.IsNullOrWhiteSpace(env) || string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentNullException("Environment Name and EntityId are required");
            }

            var plain = $"{env.ToLower()}:" + 
                   $"{module.ToString().ToLower()}:" + 
                   $"{entityId}";

            return TokenHelper.Encrypt(plain, secretKey);
        }

        public static AgoraChannelContext Decode(string channelName, string secretKey)
        {
            if (string.IsNullOrWhiteSpace(channelName))
            {
                throw new ArgumentNullException("Channel Name is required.");
            }

            var plain = TokenHelper.Decrypt(channelName, secretKey);
            var parts = plain.Split(':');
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