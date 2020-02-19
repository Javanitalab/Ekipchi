using System;
using System.IO;
using Hangfire;
using Hastnama.Ekipchi.Api.Core.Environment;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Api.Installer;
using Hastnama.Ekipchi.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs,
            IApplicationBootstrapper applicationBootstrapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            applicationBootstrapper.Initial();

            #region Static files Setting

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, ApplicationStaticPath.Avatars)),
                RequestPath = ApplicationStaticPath.Clients.Avatar
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, ApplicationStaticPath.Images)),
                RequestPath = ApplicationStaticPath.Clients.Image
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, ApplicationStaticPath.Videos)),
                RequestPath = ApplicationStaticPath.Clients.Video
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, ApplicationStaticPath.Musics)),
                RequestPath = ApplicationStaticPath.Clients.Music
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, ApplicationStaticPath.Documents)),
                RequestPath = ApplicationStaticPath.Clients.Document
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, ApplicationStaticPath.Others)),
                RequestPath = ApplicationStaticPath.Clients.Other
            });

            #endregion Static files Setting

            app.UseHangfireDashboard();


            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseHttpsRedirection();
            app.UseMiddleware<ApplicationMetaMiddleware>();
            app.UseMiddleware<MembershipMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ekipchi  API V1"); });

        }
    }
}