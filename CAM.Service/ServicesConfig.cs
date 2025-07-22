using CAM.Service.Abstractions;
using CAM.Service.Access_Service;
using CAM.Service.DataBase_Service;
using CAM.Service.DatabaseEngine_Service;
using CAM.Service.DataCenter_Service;
using CAM.Service.Repository.AccessRepo.ReadRepo;
using CAM.Service.Repository.AccessRepo.WriteRepo;
using CAM.Service.Repository.DataBaseEngineRepo;
using CAM.Service.Repository.DataBaseEngineRepo.ReadRepo;
using CAM.Service.Repository.DataBaseEngineRepo.WriteRepo;
using CAM.Service.Repository.DataBaseRepo;
using CAM.Service.Repository.DataCenterRepo.ReadRepo;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
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
            services.AddScoped<IRepoUnitOfWork, RepoUnitOfWork>();

            //write repos
            services.AddScoped<IDataCenterSqlDataRepository,DataCenterSqlDataRepository>();
            services.AddScoped<IDataBaseEngineRepo, DataBaseEngineRepo>();
            services.AddScoped<IDataBaseRepo, DataBaseRepo>();
            services.AddScoped<IAccessRepository, AccessRepository>();

            //read repos
            services.AddScoped<IReadDataCenterRepository, ReadDataCenterRepository>();
            services.AddScoped<IReadDataBaseEngineRepo, ReadDataBaseEngineRepo>();
            services.AddScoped<IReadAccessRepository, ReadAccessRepository>();




            //services
            services.AddScoped<IDataCenterService, DataCenterService>();
            services.AddScoped<IDatabaseEngineService, DatabaseEngineService>();
            services.AddScoped<IDataBaseService,DataBaseService>();
            services.AddScoped<IAccessService, AccessService>();
        }
    }
}
