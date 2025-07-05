using CAM.Service.Abstractions;
using CAM.Service.Access_Service;
using CAM.Service.DataBase_Service;
using CAM.Service.DatabaseEngine_Service;
using CAM.Service.DataCenter_Service;
using CAM.Service.Repository.AccessRepo;
using CAM.Service.Repository.DataBaseEngineRepo;
using CAM.Service.Repository.DataBaseRepo;
using CAM.Service.Repository.DataCenterRepo;
using Microsoft.Extensions.DependencyInjection;
using ReadSqlDataAccess;
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
            services.AddScoped<IRepoUnitOfWork, RepoUnitOfWork>();

            services.AddScoped<IDataCenterSqlDataRepository,DataCenterSqlDataRepository>();
            services.AddScoped<IDataBaseEngineRepo, DataBaseEngineRepo>();
            services.AddScoped<IDataBaseRepo, DataBaseRepo>();
            services.AddScoped<IAccessRepository, AccessRepository>();

            services.AddSingleton<IReadDataAccess,ReadDataAccess>();
            services.AddScoped<IReadDataCenterRepository, ReadDataCenterRepository>();

            services.AddScoped<AccessValidator>();

            services.AddScoped<IDataCenterService, DataCenterService>();
            services.AddScoped<IDatabaseEngineService, DatabaseEngineService>();
            services.AddScoped<IDataBaseService,DataBaseService>();
            services.AddScoped<IAccessService, AccessService>();
        }
    }
}
