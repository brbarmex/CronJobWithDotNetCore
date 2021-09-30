using CronJobNET.Worker.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace CronJobNET.Worker.Extensions
{
    public static class CronJobExtensionToServiceCollection
    {
        public static void AddJobs(this IServiceCollection services)
        {
            services.AddHostedService<NotificationJob>(_ => new NotificationJob(60000));
            services.AddHostedService<PurgerLogsJob>(_ => new PurgerLogsJob(120000));
        }
    }
}