using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CCDays.Konfhub.ServerlessMiddleware.NET5
{
    /// <summary>
    /// Entry point 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Configure Functions worker,services and configurations
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Task Main(string[] args)
        {
            Debugger.Launch();

            Startup startup = null;

            var host = new HostBuilder()
                .ConfigureAppConfiguration(config =>
                {
                    config.AddCommandLine(args);
                    config.AddEnvironmentVariables();
                    config.AddJsonFile("local.settings.json", true);
                })
                .ConfigureFunctionsWorker((context, builder) =>
                {
                    startup ??= new Startup(context.Configuration);
                    startup.ConfigureWorker(builder);
                })
                .ConfigureServices((context, services) =>
                {
                    startup ??= new Startup(context.Configuration);
                    startup.ConfigureServices(services);
                })
                .Build();
            return host.RunAsync();
        }
    }
}
