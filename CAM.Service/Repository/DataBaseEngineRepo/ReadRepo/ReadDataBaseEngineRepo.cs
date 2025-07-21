using CAM.Service.Dto;
using Domain.DataModels;
using ReadDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataBaseEngineRepo.ReadRepo
{
    internal class ReadDataBaseEngineRepo : IReadDataBaseEngineRepo
    {
        private readonly IReadDataAccess _readRepo;

        public ReadDataBaseEngineRepo(IReadDataAccess readDataAccess)
        {
            _readRepo = readDataAccess;
        }

        public async Task AddDataBaseEngine(string name, string address, string dcName)
        {
            await _readRepo.SaveData<dynamic>("spDataBaseEngine_Add", new { dcName = dcName, name = name, address = address });
        }

        public async Task DeleteDataBaseEngine(string name, string dcName)
        {
            await _readRepo.SaveData<dynamic>("spDataBaseEngine_Delete", new { dcName = dcName, name = name });
        }

        public async Task<DatabaseEngine> GetDatabaseEngine(string dcName, string name)
        {
            var result = await _readRepo.LoadData<DatabaseEngine, dynamic>(
            "spDataBaseEngine_Get",
            new
            {
                dcName = dcName,
                name = name
            });

            return  result.FirstOrDefault() ?? DatabaseEngine.Empty;
        }

        public async Task<IEnumerable<DatabaseEngine>> SearchDataBaseEngine(SearchDCDto searchDCDto)
        {
            return await _readRepo.LoadData<DatabaseEngine,dynamic>(
            "spDataBaseEngine_Search",
            new { 
                dcName = searchDCDto.DCSourceName,
                name = searchDCDto.DBEngineName,
                address = searchDCDto.DBEngineAddress
            });
        }
    }
}
