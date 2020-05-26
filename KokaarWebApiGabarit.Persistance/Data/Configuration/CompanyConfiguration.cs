using KokaarWebApiGabarit.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace KokaarWebApiGabarit.Persistance.Data.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
            (
                new Company
                {
                    Id = 1,
                    Name = "IT_Solutions Ltd",
                    Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                    Country = "USA",
                    CreationDate = DateTime.Now,
                    CreationUser = "application"
                },
                new Company
                {
                    Id = 2,
                    Name = "Admin_Solutions Ltd",
                    Address = "312 Forest Avenue, BF 923",
                    Country = "USA",
                    CreationDate = DateTime.Now,
                    CreationUser = "application"
                }
            );
        }

    }
}
