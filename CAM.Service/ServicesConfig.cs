using CAM.Service.DataBase_Service;
using CAM.Service.DatabaseEngine_Service;
using CAM.Service.DataCenter_Service;
using CAM.Service.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service
{
    public static class ServicesConfig
    {
        public static void AddCAMServices(this IServiceCollection services)
        {
            services.AddScoped<IDataCenterSqlDataRepository,DataCenterSqlDataRepository>();
            services.AddScoped<IDataCenterService, DataCenterService>();
            services.AddScoped<IDatabaseEngineService, DatabaseEngineService>();
            services.AddScoped<IDataBaseService,DataBaseService>();
        }
    }
}
