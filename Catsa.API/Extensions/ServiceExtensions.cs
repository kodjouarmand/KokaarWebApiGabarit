using AspNetCoreRateLimit;
using Catsa.API.CustomFormatter;
using Catsa.Domain.Entities;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Catsa.Infrastructure.Logging;
using Catsa.Domain.Data;
using Catsa.Infrastructure.Authentication;
using AutoMapper;
using Catsa.API.ActionFilters;
using Catsa.Utility.ConfigSettings;
using Catsa.BusinessLogic.Queries.Proxies;
using Catsa.BusinessLogic.Commands.Proxies;
using Microsoft.EntityFrameworkCore;

namespace Catsa.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("ConnectionStrings");
            services.AddDbContext<CatsaDbContext>(opts =>
            opts.UseSqlServer(configSection.GetValue<string>("CatsaConnectionString")));
        }

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IProxyQuery, ProxyQuery>();
            services.AddScoped<IProxyCommand, ProxyCommand>();
        }

        public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LoggingSettings>(configuration.GetSection(LoggingSettings.SectionName));
            services.Configure<DbSettings>(configuration.GetSection(DbSettings.SectionName));

        }
        public static void ConfigureControllers(this IServiceCollection services) =>
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
            }).AddXmlDataContractSerializerFormatters()
            .AddCustomCSVFormatter();

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                      builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options => { });

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerService, LoggerService>();
        }

        public static void ConfigureAuthenticationService(this IServiceCollection services) =>
            services.AddScoped<IAuthenticationService, AuthenticationService>();

        public static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(Startup));

        public static void ConfigureActionFilters(this IServiceCollection services) =>
            services.AddScoped<ValidationFilterAttribute>();

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
            
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }

        public static void ConfigureResponseCaching(this IServiceCollection services)
            => services.AddResponseCaching();

        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
            => services.AddHttpCacheHeaders(
                (expirationOpt) =>
                {
                    expirationOpt.MaxAge = 65;
                    expirationOpt.CacheLocation = CacheLocation.Private;
                },
                (validationOpt) =>
                {
                    validationOpt.MustRevalidate = true;
                }
                );

        public static void ConfigureRateLimitingOptions(this IServiceCollection services)
        {
            var rateLimitRules = new List<RateLimitRule>
                                {
                                    new RateLimitRule
                                    {
                                        Endpoint = "*",
                                        Limit= 30,
                                        Period = "5m"
                                    }
                                };
            services.Configure<IpRateLimitOptions>(opt =>
            {
                opt.GeneralRules = rateLimitRules;
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApplicationUser>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<CatsaDbContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = "CatsaSecretKey";// Environment.GetEnvironmentVariable("SECRET");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Catsa API",
                    Version = "v1",
                    Description = "Catsa.API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Armand K",
                        Email = "armand@gmail.com",
                        Url = new Uri("https://twitter.com/johndoe"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Catsa.API API LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                s.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Catsa API",
                    Version = "v2"
                });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
