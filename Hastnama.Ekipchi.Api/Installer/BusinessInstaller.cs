using Hastnama.Ekipchi.Api.Core.Token;
using Hastnama.Ekipchi.Data;
using Hastnama.Ekipchi.Data.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hastnama.Ekipchi.Api.Installer
{
    public class BusinessInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddTransient<ITokenGenerator, TokenGenerator>();

        }
    }
}