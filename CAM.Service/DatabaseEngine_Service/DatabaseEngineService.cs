using CAM.Service.Dto;
using CAM.Service.Repository;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service
{
    internal class DatabaseEngineService : IDatabaseEngineService
    {
        private readonly IDataCenterSqlDataRepository _sqlRepo;

        public DatabaseEngineService(IDataCenterSqlDataRepository sqlDataRepository)
        {
            _sqlRepo = sqlDataRepository;
        }


        public async Task AddDatabaseEngine(string dcName, string dbEngineName, string address)
        {
           await _sqlRepo.AddDataBaseEngine(dcName, dbEngineName, address);
        }

        public async Task Remove(string dcName, string engineName)
        {
            await _sqlRepo.RemoveDataBaseEngine(dcName, engineName);
        }

        public Task<List<DatabaseEngine>> Search(SearchDbEngineDto searchDto)
        {
            return _sqlRepo.SearchDataBaseEngine(searchDto);
        }
    }
}
