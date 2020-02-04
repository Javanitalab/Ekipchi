using Hastnama.Ekipchi.Api.Core.Environment;
using Hastnama.Ekipchi.Api.Core.Token;
using Hastnama.Ekipchi.Api.Filter;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Business.Service.Class;
using Hastnama.Ekipchi.Business.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hastnama.Ekipchi.Api.Installer
{
    public class BusinessInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<AdminAuthorization>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddSingleton<IRequestMeta, RequestMeta>();


        }
    }
}