using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace MyApp
{
    public class Program
    {
        public static void Main(string[] arguments)
        {
            var host = CreateHostBuilder(arguments).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] arguments) =>
            Host.CreateDefaultBuilder(arguments)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}