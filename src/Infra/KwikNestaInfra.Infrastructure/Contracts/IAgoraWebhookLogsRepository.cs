using KwikNestaInfra.Domain.Entities;
using System.Linq.Expressions;

namespace KwikNestaInfra.Infrastructure.Contracts
{
    public interface IAgoraWebhookLogsRepository
    {
        IQueryable<AgoraWebhookEventLog> Get(Expression<Func<AgoraWebhookEventLog, bool>> predicate);
        Task<bool> TryInsertAsync(AgoraWebhookEventLog log);
    }
}