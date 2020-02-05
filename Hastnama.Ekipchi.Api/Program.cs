using Hastnama.Ekipchi.Api.Data;
using Hastnama.Ekipchi.DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hastnama.Ekipchi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var scope = CreateHostBuilder(args).Build().Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                services.GetRequiredService<EkipchiDbContext>();

                SeedData.Initialize(services);
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
