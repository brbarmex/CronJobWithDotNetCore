using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;

namespace CronJobNET.Worker.Bases
{
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        protected BackgroundService(string cronExpression, TimeZoneInfo timeZoneInfo)
        {
            _cronExpression = CronExpression.Parse(cronExpression);
            _timeZoneInfo = timeZoneInfo;
        }

        private System.Timers.Timer _timer;
        private readonly CronExpression _cronExpression;
        private readonly TimeZoneInfo _timeZoneInfo;

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            var nextOccurrence = _cronExpression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);

            if (!nextOccurrence.HasValue)
            {
                await Task.CompletedTask;
                return;
            }

            var delayTotalMilliseconds = (nextOccurrence.Value - DateTimeOffset.Now).TotalMilliseconds;

            if (delayTotalMilliseconds <= 0)
                await StartAsync(cancellationToken);

            _timer = new System.Timers.Timer(delayTotalMilliseconds);
            _timer.Elapsed += async (sender, arq) => await ExecuteAsync(cancellationToken);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Start();
            await Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            await Task.CompletedTask;
        }


        public virtual void Dispose()
        {
            try
            {
                _timer?.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occured when disposed a object see details:{ex.Message}");
                Console.ResetColor();
            }
        }
    }
}