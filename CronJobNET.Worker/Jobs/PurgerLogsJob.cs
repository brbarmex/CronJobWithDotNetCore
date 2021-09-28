using System;
using System.Threading;
using System.Threading.Tasks;
using CronJobNET.Worker.Bases;

namespace CronJobNET.Worker.Jobs
{
    public class PurgerLogsJob : BackgroundService
    {
        public PurgerLogsJob(string cronExpression, TimeZoneInfo timeZoneInfo) : base(cronExpression, timeZoneInfo)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            var time = DateTime.Now;
            Console.WriteLine($"{time} PurgerLogsJob - Purging logs from E-Mail");
            Console.WriteLine($"{time} PurgerLogsJob - Purging logs from SMS");
            Console.ResetColor();
            await Task.CompletedTask;
        }
    }
}