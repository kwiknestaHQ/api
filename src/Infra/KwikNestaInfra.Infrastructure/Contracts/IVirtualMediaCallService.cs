using Hangfire.Server;

namespace KwikNestaInfra.Infrastructure.Contracts
{
    public interface IVirtualMediaCallService
    {
        Task ProcessAgoraWebhook(string body, PerformContext context);
    }
}
