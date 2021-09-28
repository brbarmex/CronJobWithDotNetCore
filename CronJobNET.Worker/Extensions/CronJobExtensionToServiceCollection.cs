using CronJobNET.Worker.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace CronJobNET.Worker.Extensions
{
    public static class CronJobExtensionToServiceCollection
    {
        public static void AddJobs(this IServiceCollection services)
        {
            services.AddHostedService<NotificationJob>(_ => new NotificationJob("*/1 * * * *", System.TimeZoneInfo.Local));
            services.AddHostedService<PurgerLogsJob>(_ => new PurgerLogsJob("*/2 * * * *", System.TimeZoneInfo.Local));
        }
    }
}