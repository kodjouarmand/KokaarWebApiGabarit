using Catsa.Domain.Entities;
using Catsa.Domain.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Catsa.Domain.Data
{
    public class CatsaDbContext : IdentityDbContext<ApplicationUser>
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
