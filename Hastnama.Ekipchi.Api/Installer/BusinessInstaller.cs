using Hastnama.Ekipchi.Api.Core.Email;
using Hastnama.Ekipchi.Api.Core.Environment;
using Hastnama.Ekipchi.Api.Core.FileProcessor;
using Hastnama.Ekipchi.Api.Core.Token;
using Hastnama.Ekipchi.Api.Filter;
using Hastnama.Ekipchi.Business.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hastnama.Ekipchi.Api.Installer
{
    public class BusinessInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.Configure<ThumbnailSize>(configuration.GetSection("ThumbnailSize"));
            services.Configure<FileProvider>(configuration.GetSection("FileProvider"));
            services.Configure<HostAddress>(configuration.GetSection("HostAddress"));
            services.Configure<EmailSetting>(configuration.GetSection("EmailSetting"));

            
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<AdminAuthorization>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IImageProcessingService, ImageProcessingService>();
            services.AddTransient<IEmailServices, EmailServices>();

            services.AddSingleton<IRequestMeta, RequestMeta>();


        }
    }
}