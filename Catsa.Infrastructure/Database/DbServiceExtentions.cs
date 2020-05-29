using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Catsa.Domain.Data;
using Catsa.DataAccess.Repositories;
using Catsa.DataAccess.Repositories.Contracts;
using Catsa.Utility.ConfigSettings;
using Microsoft.Extensions.Options;

namespace Catsa.Infrastructure.Database
{
    public static class DbServiceExtention
    {        
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<ICatsaDbUnitOfWork, CatsaDbUnitOfWork>();
        }
    }
}
