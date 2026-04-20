using Hangfire.RecurringJobExtensions;
using Hangfire.Server;

namespace KwikNestaGateway.API.Services
{
    public interface IRecurringJobService
    {
        //[RecurringJob("*/2 * * * *")]
        Task RunPaymentVerifications(PerformContext context);
    }
}