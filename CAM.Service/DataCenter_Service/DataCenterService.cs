using CAM.Service.Abstractions;
using CAM.Service.Dto;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service
{
    internal class DataCenterService : IDataCenterService
    {
        private readonly IDataCenterSqlDataRepository _sqlRepo;
        public DataCenterService(IDataCenterSqlDataRepository repository)
        {
            _sqlRepo = repository;
        }

        public async Task CreateDataCenterByName(string name)
        {
            await _sqlRepo.AddDataCenter(DataCenter.CreateByName(name));
        }

        public async Task DeleteDataCenter(string name)
        {
            await _sqlRepo.DeleteDataCenter(name);
        }

        public async Task EditDataCenterName(string oldName, string newName)
        {
            await _sqlRepo.UpdateDataCenter(oldName, newName);
        }

        public async Task<List<DataCenter>> GetAllDataCenters()
        {
            return await _sqlRepo.GetAllDataCenters();
        }

        public async Task<DataCenter> GetDataCenter(string name)
        {
            return await _sqlRepo.GetDataCenter(name);
        }
        public async Task<DataCenter> GetDataCenterWithDatabaseEngines(string name)
        {
            SearchDCDto searchDCDto = new SearchDCDto.Create()
                .AddSourceDcName(name)
                .Build();

            return await _sqlRepo.SearchDataCenter<BasePredicateBuilder>(searchDCDto);
        }
    }
}
