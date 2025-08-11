using ApplicationDbContext.EntityConfigs;
using Domain.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDbContext.DbContexts
{
    public class WriteTenantDbContext : DbContext
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IConfiguration _configuration;

        public WriteTenantDbContext(
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            DbContextOptions<WriteTenantDbContext> options) : base(options)
        {
            _accessor = httpContextAccessor;
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

            string tenantId = _accessor.HttpContext.User.Claims.First(c => c.Type == "tenant-id").Value ?? string.Empty;
            if (tenantId != string.Empty)
            {
                var connectionString = string.Format(dbOptions.sqlServer.TenantConnectionString, tenantId);

                optionsBuilder.UseSqlServer(
                connectionString
                      , sqlServerOptionsAction =>
                      {
                          sqlServerOptionsAction.EnableRetryOnFailure(
                              maxRetryCount: dbOptions.sqlServer.retryCount
                              );
                      });
            }
        }
    }
}
