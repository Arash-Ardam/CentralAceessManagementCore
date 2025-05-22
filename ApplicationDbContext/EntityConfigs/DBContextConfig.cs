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
    public static class DBContextConfig
    {
        public static void AddCAMDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            var config = configuration.GetRequiredSection("DbContextConfigEntry");
            var dbOptions = config.Get<DbContextConfigEntry>();
            if (default == dbOptions)
            {
                throw new NullReferenceException(nameof(dbOptions));
            }

            if (dbOptions.isEnabled && dbOptions.sqlServer.isEnabled)
            {
                services.AddDbContext<ApplicationDbContext>(x =>
                    x.UseSqlServer(
                        dbOptions.sqlServer.ConnectionString
                        , sqlServerOptionsAction =>
                        {
                            sqlServerOptionsAction.EnableRetryOnFailure(
                                maxRetryCount: dbOptions.sqlServer.retryCount
                                );

                        }), ServiceLifetime.Scoped
                        );
            }
        }
    }

    public class DbContextConfigEntry
    {
        public bool isEnabled { get; set; }
        public sqlServer sqlServer { get; set; } = new sqlServer();
    }
    public class sqlServer
    {
        public bool isEnabled { get; set; }
        public short retryCount { get; set; }
        public string ConnectionString { get; set; } = string.Empty;
    }
}
