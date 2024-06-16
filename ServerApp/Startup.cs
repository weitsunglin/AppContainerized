using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Services;

namespace MyApp
{
    public class Startup
    {
        // 建構函數，注入配置
        public Startup(IConfiguration configuration)
        {
            AppConfiguration = configuration;
        }

        // 保存應用程式配置的屬性
        public IConfiguration AppConfiguration { get; }

        // 註冊應用程式所需的服務
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            // 添加控制器支持
            serviceCollection.AddControllers();
            
            // 註冊單例服務
            serviceCollection.AddSingleton<HttpRequestHandler>();
            serviceCollection.AddSingleton<DatabaseHandler>();
            serviceCollection.AddSingleton<ISocketService, SocketService>();
        }

        // 配置應用程式的請求處理管道
        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment webHostEnvironment, ISocketService socketService)
        {
            // 如果是開發環境，使用開發者異常頁面
            if (webHostEnvironment.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
            }

            // 啟用路由中間件
            appBuilder.UseRouting();

            // 配置端點映射
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 啟動 Socket 服務
            _ = socketService.StartAsync();
        }
    }
}
