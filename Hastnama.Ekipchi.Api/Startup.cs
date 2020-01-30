using Hastnama.Ekipchi.Api.Core.Environment;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Api.Installer;
using Hastnama.Ekipchi.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hastnama.Ekipchi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesAssembly(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IApplicationBootstrapper applicationBootstrapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            applicationBootstrapper.Initial();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();
            app.UseMiddleware<ApplicationMetaMiddleware>();
            app.UseMiddleware<MembershipMiddleware>();
            app.UseAuthentication();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ekipchi  API V1"); });
            
        }
    }
}