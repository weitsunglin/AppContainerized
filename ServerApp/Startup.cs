using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Modules;
using MyApp.Services;


namespace MyApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppConfiguration = configuration;
        }

        public IConfiguration AppConfiguration { get; }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers();
            
            serviceCollection.AddSingleton<HttpRequestHandler>();
            serviceCollection.AddSingleton<DatabaseHandler>();
            serviceCollection.AddSingleton<SocketService>();

            // Register module services dynamically
            serviceCollection.AddSingleton<IModule, ModuleA>();
            serviceCollection.AddSingleton<IModule, ModuleB>();
            serviceCollection.AddSingleton<IModule, ModuleC>();
        }

        public void Configure(IApplicationBuilder appBuilder, IWebHostEnvironment webHostEnvironment, SocketService socketService)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
            }

            appBuilder.UseRouting();

            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            _ = socketService.StartAsync();
        }
    }
}