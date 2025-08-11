using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadAppDbContext.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAppDbContext.Configs
{
    public static class ReadDbConfig
    {

        public static void AddReadDbContexts(this IServiceCollection services, IConfiguration configuration)
        {

            var config = configuration.GetRequiredSection(nameof(ReadDbOptions));
            var dbOptions = config.Get<ReadDbOptions>();
            if (default == dbOptions)
            {
                throw new NullReferenceException(nameof(dbOptions));
            }

            if (dbOptions.isEnabled && dbOptions.sqlServer.isEnabled)
            {
                services.AddDbContext<ReadTenantDbContext>(ServiceLifetime.Scoped);
            }
        }
    }
}
