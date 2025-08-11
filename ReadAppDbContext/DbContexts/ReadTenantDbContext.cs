using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadAppDbContext.Configs;
using ReadAppDbContext.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAppDbContext.DbContexts
{
    public class ReadTenantDbContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _accessor;


        public ReadTenantDbContext(
            DbContextOptions<ReadTenantDbContext> dbContextOptions,
            IServiceProvider serviceProvider,
            IHttpContextAccessor contextAccessor) : base(dbContextOptions)
        {
            _serviceProvider = serviceProvider;
            _accessor = contextAccessor;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReadAccess>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ReadDBEngine>()
                .HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            var config = configuration.GetRequiredSection(nameof(ReadDbOptions));
            var dbOptions = config.Get<ReadDbOptions>();

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
