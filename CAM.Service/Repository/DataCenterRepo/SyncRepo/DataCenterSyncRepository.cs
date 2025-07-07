using CAM.Service.DataCenter_Service.Queries;
using CAM.Service.Repository.DataCenterRepo.ReadRepo;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataCenterRepo.SyncRepo
{
    internal class DataCenterSyncRepository : IDataCenterSyncRepository
    {
        private readonly IDataCenterSqlDataRepository _writeRepo;
        private readonly IReadDataCenterRepository _readRepo;

        public DataCenterSyncRepository(IDataCenterSqlDataRepository writeRepo,IReadDataCenterRepository readRepo)
        {
            _writeRepo = writeRepo;
            _readRepo = readRepo;
        }

        public async Task SyncAsync(string dataCenterName)
        {
            DataCenter ExcistedDc = await _writeRepo.GetDataCenter(dataCenterName) ?? DataCenter.Empty;
            var ReadedEntities = await _readRepo.GetDataCenter(dataCenterName) ?? DataCenter.Empty;

            if (ExcistedDc == DataCenter.Empty && ReadedEntities != DataCenter.Empty)
                await _readRepo.DeleteDataCenter(dataCenterName);

            else if (ExcistedDc != DataCenter.Empty && ReadedEntities == DataCenter.Empty)
                await _readRepo.AddDataCenter(dataCenterName);
        }
    }
}
