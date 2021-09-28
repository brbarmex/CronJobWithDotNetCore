using CronJobNET.Worker.Extensions;
using Microsoft.Extensions.Hosting;

namespace CronJobNET.Worker
{
    public static class Program
    {
        public static void Main(string[] args)
        =>  Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddJobs())
                .Build()
                .Run();
    }
}
