using ApplicationDbContext.EntityConfigs;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDbContext.DbContexts
{
    public class AdminDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AdminDbContext(IConfiguration configuration,DbContextOptions<AdminDbContext> dbContextOptions) : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Database>()
                           .HasKey(e => e.Name);

            modelBuilder.Entity<Database>()
                .Property<string>("DataBaseEngineName")
                .IsRequired();

            modelBuilder.Entity<Access>()
                .HasKey(ac => ac.Id);

            modelBuilder.Entity<Access>()
                .Property<string>("DataCenterName")
                .IsRequired();

            modelBuilder.ApplyConfiguration(new DataCenterConfigs());
            modelBuilder.ApplyConfiguration(new DatabaseEngineConfigs());

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = _configuration.GetRequiredSection("WriteDbContextConfigEntry");
            var dbOptions = config.Get<WriteDbContextConfigEntry>();

            optionsBuilder.UseSqlServer(
                        dbOptions.sqlServer.AdminConnectionString
                        , sqlServerOptionsAction =>
                        {
                            sqlServerOptionsAction.EnableRetryOnFailure(
                                maxRetryCount: dbOptions.sqlServer.retryCount
                                );

                        });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
