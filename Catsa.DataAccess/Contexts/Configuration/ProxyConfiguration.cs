using Catsa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Catsa.DataAccess.Contexts.Configuration
{
    public class ProxyConfiguration : IEntityTypeConfiguration<Proxy>
    {
        public void Configure(EntityTypeBuilder<Proxy> builder)
        {
            builder.HasData
            (
                new Proxy
                {
                    Id = Guid.NewGuid(),
                    Nom = "Bravo",
                    Description = "Proxy de type REST",
                    Type = "REST",
                    CreationDate = DateTime.Now,
                    CreationUser = "application"
                },
                new Proxy
                {
                    Id = Guid.NewGuid(),
                    Nom = "Alpha",
                    Description = "Proxy de type SOAP",
                    Type = "SOAP",
                    CreationDate = DateTime.Now,
                    CreationUser = "application"
                }
            );
        }

    }
}
