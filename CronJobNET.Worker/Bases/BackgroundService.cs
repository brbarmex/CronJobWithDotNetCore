using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;

namespace CronJobNET.Worker.Bases
{
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        protected BackgroundService(double timeInMiliseconds)
        => _timeInMiliseconds = timeInMiliseconds;

        private System.Timers.Timer _timer;
        private readonly double _timeInMiliseconds;

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new System.Timers.Timer(_timeInMiliseconds);
            _timer.Elapsed += async (sender, arq) => await ExecuteAsync(cancellationToken);
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