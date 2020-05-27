using Catsa.Domain.Entities;
using Catsa.DataAccess.Contexts.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Catsa.DataAccess.Contexts
{
    public class CatsaDbContext : IdentityDbContext<UserAccount>
    {
        public CatsaDbContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProxyConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Proxy> Proxies { get; set; }
    }

}
