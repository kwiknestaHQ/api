using KwikNestaInfra.Domain.Entities;
using KwikNestaInfra.Infrastructure.Contracts;
using KwikNestaInfra.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KwikNestaInfra.Infrastructure.Repositories
{
    public class AgoraWebhookLogsRepository(InfraServiceDbContext context)
        : IAgoraWebhookLogsRepository
    {
        private readonly InfraServiceDbContext _context = context;

        public async Task<bool> TryInsertAsync(AgoraWebhookEventLog log)
        {
            var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                INSERT INTO ""kn-infra-svc"".""AgoraWebhookEvents""(""Id""< ""Channel"", ""Type"", ""Module"", ""Timestamp"", ""UId"", ""Platform"", ""Duration"", ""Reason"", ""CreatedAt"")
                VALUES ({log.Id}, {log.Channel}, {log.Type}, {log.Module}, {log.Timestamp}, {log.UId}, {log.Platform}, {log.Duration}, {log.Reason}, {log.CreatedAt})
                ON CONFLICT DO NOTHING;
            ");

            return rows > 0;
        }

        public IQueryable<AgoraWebhookEventLog> Get(Expression<Func<AgoraWebhookEventLog, bool>> predicate)
        {
            return _context.AgoraWebhookEvents.Where(predicate);
        }
    }
}