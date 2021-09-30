using System;
using System.Threading;
using System.Threading.Tasks;
using CronJobNET.Worker.Bases;

namespace CronJobNET.Worker.Jobs
{
    public class NotificationJob : BackgroundService
    {
        public NotificationJob(double timeInMiliseconds) : base(timeInMiliseconds)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            var time = DateTime.Now;
            Console.WriteLine($"{time} NotificationJob - Send E-Mail");
            Console.WriteLine($"{time} NotificationJob - Send SMS");
            Console.ResetColor();
            await Task.CompletedTask;
        }
    }
}
