using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadDbContext
{
    public static class ReadDbContextConfiguration
    {
        public static void AddCAMReadDbContext(this IServiceCollection services)
        {

            services.AddSingleton<IReadDataAccess, ReadDataAccess>();
        }
    }
}
