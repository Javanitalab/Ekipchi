using Hastnama.Ekipchi.Api.Core.Logger;
using Hastnama.Ekipchi.Api.Data;
using Hastnama.Ekipchi.DataAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Hastnama.Ekipchi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            #region SeriLog

            var seriLogSetting = new SeriLogSetting();
            config.GetSection("SeriLogSetting").Bind(seriLogSetting);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Information()
                .WriteTo.Seq(seriLogSetting.Address)
                .CreateLogger();
            Log.Information(" SeriLog Initialized on {Address} ... ", seriLogSetting.Address);

            #endregion


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
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseUrls("http://localhost:80").UseStartup<Startup>(); });
    }
}