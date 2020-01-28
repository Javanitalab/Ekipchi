using Hastnama.Ekipchi.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hastnama.Ekipchi.Api.Installer
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            var connection = configuration.GetConnectionString("EkipchiDbConnection");
            services.AddDbContext<EkipchiDbContext>(options =>
                options.UseSqlServer(connection));
        }
    }
}