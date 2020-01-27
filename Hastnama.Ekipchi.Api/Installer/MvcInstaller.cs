using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Hastnama.Ekipchi.Api.Core.Environment;
using Hastnama.Ekipchi.Api.InfraStructure;
using Hastnama.Ekipchi.Data.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Hastnama.Ekipchi.Api.Installer
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IApplicationBootstrapper, ApplicationBootstrapper>();
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                    builder => { builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); });
            });

            services.AddControllers();

            #region AuthToken

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            var jwtSetting = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSetting);
            services.AddSingleton(jwtSetting);

            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting.Secret)),
                ValidateIssuer = false,
                ValidIssuer = jwtSetting.ValidIssuer,
                ValidateAudience = false,
                ValidAudience = jwtSetting.ValidAudience,
                ValidateLifetime = true,
                RequireExpirationTime = false
            };
            services.AddSingleton(tokenValidationParameter);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameter;
                });

            #endregion

            #region Automapper

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region Swagger

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ekipchi  API",
                    Version = "v1.0",
                    Description = "Ekipchi ASP.NET Core Web API",
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            #endregion
        }
    }
}