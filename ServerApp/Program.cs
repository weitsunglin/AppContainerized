using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MyApp
{
    public class Program
    {
        // 應用程式的進入點
        public static void Main(string[] arguments)
        {
            // 建立並運行主機
            CreateHostBuilder(arguments).Build().Run();
        }

        // 配置主機生成器
        public static IHostBuilder CreateHostBuilder(string[] arguments) =>
            Host.CreateDefaultBuilder(arguments)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // 設定 Startup 類來配置應用程式
                    webBuilder.UseStartup<Startup>();
                });
    }
}
