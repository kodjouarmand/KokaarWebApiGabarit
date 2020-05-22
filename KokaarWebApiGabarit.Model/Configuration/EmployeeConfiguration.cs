using KokaarWebApiGabarit.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KokaarWebApiGabarit.Model.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
            (
            new Employee
            {
                Name = "Sam Raiden",
                Age = 26,
                Position = "Software developer",
                CompanyId = 1
            },
            new Employee
            {
                Name = "Jana McLeaf",
                Age = 30,
                Position = "Analyst",
                CompanyId = 1
            },
            new Employee
            {
                Name = "Kane Miller",
                Age = 35,
                Position = "Administrator",
                CompanyId = 2
            }
            );
        }
    }

}
