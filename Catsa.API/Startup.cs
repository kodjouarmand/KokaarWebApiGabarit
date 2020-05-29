using System.IO;
using AspNetCoreRateLimit;
using Catsa.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Catsa.Infrastructure.Logging;
using Catsa.Infrastructure.Database;

namespace Catsa.API
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
            services.ConfigureSettings(Configuration);
            services.ConfigureLoggerService();
            services.ConfigureDbContexts(Configuration);
            services.ConfigureAuthenticationService();
            services.ConfigureBusinessServices();
            services.ConfigureCors();
            services.ConfigureIISIntegration();                        
            services.ConfigureUnitOfWork();            
            services.ConfigureAutoMapper();
            services.ConfigureActionFilters();
            services.ConfigureVersioning();
            services.ConfigureResponseCaching();
            services.ConfigureHttpCacheHeaders();
            services.AddMemoryCache();
            services.ConfigureRateLimitingOptions(); 
            services.AddHttpContextAccessor();
            services.AddAuthentication(); 
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);
            services.ConfigureAuthenticationService();
            services.ConfigureSwagger();
            services.ConfigureControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerService logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();

            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseIpRateLimiting();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Kokaar API v1");
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
