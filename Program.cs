using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace MySocketHttpApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // 啟動 Socket 服務器
            var socketServer = new SocketServer();
            var socketTask = socketServer.StartAsync();

            // 啟動 ASP.NET Core
            await host.RunAsync();

            // 停止 Socket 服務器
            socketServer.Stop();
            await socketTask;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
