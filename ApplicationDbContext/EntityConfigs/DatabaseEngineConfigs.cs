using Domain.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDbContext.EntityConfigs
{
    public class DatabaseEngineConfigs : IEntityTypeConfiguration<DatabaseEngine>
    {
        public void Configure(EntityTypeBuilder<DatabaseEngine> modelBuilder)
        {
            modelBuilder
                .HasKey(e => e.Name);

            modelBuilder
                .Property<string>("DataCenterName")
                .IsRequired();

            modelBuilder
                .HasMany(x => x.Databases)
                .WithOne()
                .HasForeignKey("DataBaseEngineName")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
