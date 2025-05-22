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
    public class DataCenterConfigs : IEntityTypeConfiguration<DataCenter>
    {
        public void Configure(EntityTypeBuilder<DataCenter> modelBuilder)
        {
            modelBuilder
                .HasKey(dc => dc.Name); 

            modelBuilder
                .HasMany(x => x.DatabaseEngines)
                .WithOne()
                .HasForeignKey("DataCenterName")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(x => x.Accesses)
                .WithOne()
                .HasForeignKey("DataCenterName")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
