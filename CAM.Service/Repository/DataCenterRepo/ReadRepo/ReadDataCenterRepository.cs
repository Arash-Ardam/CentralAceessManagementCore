using Domain.DataModels;
using ReadDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataCenterRepo.ReadRepo
{
    internal class ReadDataCenterRepository : IReadDataCenterRepository
    {
        private readonly IReadDataAccess _readDataAccess;

        public ReadDataCenterRepository(IReadDataAccess readDataAccess)
        {
            _readDataAccess = readDataAccess;
        }

        public async Task AddDataCenter(string name)
        {
            var dataCenter = await GetDataCenter(name);
            if (dataCenter == default)
                await _readDataAccess.SaveData("spDataCenter_Add", new { name = name });
        }

        public async Task DeleteDataCenter(string name)
        {
            var dataCenter = await GetDataCenter(name);
            if (dataCenter != DataCenter.Empty)
                await _readDataAccess.SaveData("spDataCenter_Delete", new { name = name });

        }

        public async Task<List<DataCenter>> GetAllDataCenters()
        {
            var dataCenters = await _readDataAccess.LoadData<DataCenter, dynamic>("spDataCenter_GetAll", new {});
            return dataCenters.ToList();
        }

        public async Task<DataCenter?> GetDataCenter(string name)
        {
            var dataCenters = await _readDataAccess.LoadData<DataCenter, dynamic>("spDataCenter_Get", new { name });
            return dataCenters.FirstOrDefault();
        }

        public async Task<DataCenter> GetDataCenterWithDatabaseEngines(string name)
        {
            var dataCenterResults = await _readDataAccess.LoadData<DataCenter, dynamic>("spDataCenter_Get", new { name });
            var dataCenter = dataCenterResults.FirstOrDefault() ?? DataCenter.Empty;   

            dataCenter.DatabaseEngines =  _readDataAccess.LoadData<DatabaseEngine, dynamic>(
                "spDataCenter_GetWithDataBaseEngines",
                new { name })
                .Result
                .ToList();

            return dataCenter ?? DataCenter.Empty;
        }
    }
}
