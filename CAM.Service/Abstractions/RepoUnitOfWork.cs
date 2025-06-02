using CAM.Service.Repository.AccessRepo;
using CAM.Service.Repository.DataBaseEngineRepo;
using CAM.Service.Repository.DataBaseRepo;
using CAM.Service.Repository.DataCenterRepo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Abstractions
{
    internal class RepoUnitOfWork : IRepoUnitOfWork
    {
        private IServiceProvider _serviceProvider;

        public RepoUnitOfWork( IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDataCenterSqlDataRepository DataCenterRepo => GetRepository<IDataCenterSqlDataRepository>();

        public IAccessRepository AccessRepository => GetRepository<IAccessRepository>();

        public IDataBaseEngineRepo DataBaseEngineRepo => GetRepository<IDataBaseEngineRepo>();

        public IDataBaseRepo DataBaseRepo => GetRepository<IDataBaseRepo>();



        private TRepository GetRepository<TRepository>()
        {
            var repository = _serviceProvider.GetService<TRepository>();

            return repository;
        }

    }
}
