using CAM.Service.Dto;
using CAM.Service.Repository;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataBase_Service
{
    internal class DataBaseService : IDataBaseService
    {
        private readonly IDataCenterSqlDataRepository _sqlRepo;

        public DataBaseService(IDataCenterSqlDataRepository repository)
        {
            _sqlRepo = repository;
        }

        public async Task AddDataBaseToDataBaseEngine(string dcName, string engineName, string dbName)
        {
            await _sqlRepo.AddDataBaseToDataBaseEngine(dcName, engineName, dbName);
        }

        public async Task<List<Database>> SearchDataBase(SearchDataBaseSto searchDto)
        {
            return await _sqlRepo.SearchDataBase(searchDto.DataCenterName, 
                searchDto.DataBaseEngineName, searchDto.DataBaseName);
        }
    }
}
