using ApplicationDbContext.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDbContext.EntityConfigs
{
    public static class WriteDBContextConfig
    {
        public static void AddCAMDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            var config = configuration.GetRequiredSection("WriteDbContextConfigEntry");
            var dbOptions = config.Get<WriteDbContextConfigEntry>();
            if (default == dbOptions)
            {
                throw new NullReferenceException(nameof(dbOptions));
            }

            if (dbOptions.isEnabled && dbOptions.sqlServer.isEnabled)
            {
                services.AddDbContext<ApplicationDbContext>(x =>
                    x.UseSqlServer(
                        dbOptions.sqlServer.AdminConnectionString
                        , sqlServerOptionsAction =>
                        {
                            sqlServerOptionsAction.EnableRetryOnFailure(
                                maxRetryCount: dbOptions.sqlServer.retryCount
                                );

                        }), ServiceLifetime.Scoped
                        );
            }
        }

        public static void AddWriteDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetRequiredSection("WriteDbContextConfigEntry");
            var dbOptions = config.Get<WriteDbContextConfigEntry>();
            if (default == dbOptions)
            {
                throw new NullReferenceException(nameof(dbOptions));
            }

            if (dbOptions.isEnabled && dbOptions.sqlServer.isEnabled)
            {
                services.AddDbContext<AdminDbContext>(ServiceLifetime.Scoped);
                services.AddDbContext<WriteTenantDbContext>(ServiceLifetime.Scoped);
            }
        }
    }

    public class WriteDbContextConfigEntry
    {
        public bool isEnabled { get; set; }
        public sqlServer sqlServer { get; set; } = new sqlServer();
    }
    public class sqlServer
    {
        public bool isEnabled { get; set; }
        public short retryCount { get; set; }
        public string AdminConnectionString { get; set; } = string.Empty;
        public string TenantConnectionString { get; set; } = string.Empty;
    }
}
