using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Catsa.DataAccess.Contexts;
using Catsa.DataAccess.Repositories;
using Catsa.DataAccess.Repositories.Contracts;
using Catsa.BusinessLogic.Queries.Proxies;
using Catsa.BusinessLogic.Commands.Proxies;

namespace Catsa.Infrastructure.Configuration
{
    public static class DbConfigurationService
    {        
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration, string connectionString) =>
            services.AddDbContext<CatsaDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString(connectionString), b =>
                b.MigrationsAssembly("Catsa.API")));

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICatsaDbUnitOfWork, CatsaDbUnitOfWork>();
            services.AddScoped<IProxyQuery, ProxyQuery>();
            services.AddScoped<IProxyCommand, ProxyCommand>();
        }
    }
}
