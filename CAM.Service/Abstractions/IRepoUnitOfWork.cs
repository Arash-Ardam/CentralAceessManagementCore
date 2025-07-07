using CAM.Service.Repository.AccessRepo;
using CAM.Service.Repository.DataBaseEngineRepo;
using CAM.Service.Repository.DataBaseRepo;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Abstractions
{
    public interface IRepoUnitOfWork
    {
        IDataCenterSqlDataRepository DataCenterRepo { get; }
        IAccessRepository AccessRepository { get; }
        IDataBaseEngineRepo DataBaseEngineRepo { get; }
        IDataBaseRepo DataBaseRepo { get; }
    }
}
