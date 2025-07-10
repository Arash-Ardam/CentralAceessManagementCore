using CAM.Service.Abstractions;
using CAM.Service.Dto;
using CAM.Service.Repository.DataBaseEngineRepo.WriteRepo;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataBaseEngineRepo
{
    internal class DataBaseEngineRepo : IDataBaseEngineRepo
    {
        private readonly ApplicationDbContext.ApplicationDbContext _dbContext;
        private readonly IDataCenterSqlDataRepository _dataCenterRepo;

        public DataBaseEngineRepo(ApplicationDbContext.ApplicationDbContext dbContext, IDataCenterSqlDataRepository dataCenterRepo)
        {
            _dbContext = dbContext;
            _dataCenterRepo = dataCenterRepo;
        }

        public async Task AddDataBaseEngine(string dcName, string dbEngineName, string address)
        {
            var dataCenter = await CheckDuplicatedDbEngines(dcName, dbEngineName, address);

            dataCenter.AddDatabaseEngine(DatabaseEngine.CreateByNameAndAddress(dbEngineName, address));

            _dbContext.Entry(dataCenter)
                      .Collection(dc => dc.DatabaseEngines)
                      .IsModified = true;

            await SaveChangesAsync();
        }

        public Task<DatabaseEngine> GetDataBaseEngine(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DatabaseEngine>> SearchDataBaseEngine(SearchDbEngineDto searchDto)
        {
            SearchDCDto searchDCDto = new SearchDCDto.Create()
                .AddSourceDcName(searchDto.dcName)
                .AddDbEngineName(searchDto.dbEngineName)
                .AddDbEngineAddress(searchDto.Address)
                .Build();

            DataCenter? dataCenter = await _dataCenterRepo.SearchDataCenter<BasePredicateBuilder>(searchDCDto);

            if (dataCenter == DataCenter.Empty)
                return new List<DatabaseEngine>();

            if (searchDto.withDatabases)
            {
                foreach (DatabaseEngine engine in dataCenter.DatabaseEngines)
                {
                    _dbContext.Entry(engine)
                        .Collection(e => e.Databases)
                        .Query()
                        .ToList();
                }
            }

            return dataCenter.DatabaseEngines;
        }

        public async Task RemoveDataBaseEngine(string dcName, string dbEngineName)
        {
            SearchDCDto searchDCDto = new SearchDCDto.Create()
                .AddSourceDcName(dcName)
                .AddDbEngineName(dbEngineName)
                .Build();

            DataCenter dataCenter = await _dataCenterRepo.SearchDataCenter<BasePredicateBuilder>(searchDCDto);

            if (dataCenter == default)
            {
                throw new Exception();
            }

            else if (dataCenter.DatabaseEngines.Count == 0)
            {
                throw new Exception();
            }

            dataCenter.DatabaseEngines.Remove(dataCenter.DatabaseEngines[0]);

            _dbContext.Entry(dataCenter).Collection(dc => dc.DatabaseEngines).IsModified = true;

            await _dbContext.SaveChangesAsync();
        }

        private async Task<DataCenter> CheckDuplicatedDbEngines(string dcName, string dbEngineName, string address)
        {
            SearchDCDto dCDto = new SearchDCDto.Create()
                .AddSourceDcName(dcName)
                .AddDbEngineName(dbEngineName)
                .AddDbEngineAddress(address)
                .Build();

            DataCenter dataCenter = await _dataCenterRepo.SearchDataCenter<BasePredicateBuilder>(dCDto);

            if (dataCenter.DatabaseEngines.Count != 0)
            {
                bool isDbEngineNameDuplicated = dataCenter.DatabaseEngines[0].Name == dbEngineName;
                bool isAddressDuplicated = dataCenter.DatabaseEngines[0].Address == address;

                if (isDbEngineNameDuplicated)
                    throw new Exception($"Entity with Param : {dbEngineName} Already Exist");
                if (isAddressDuplicated)
                    throw new Exception($"Entity with Param : {address} Already Exist");
            }

            return dataCenter;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
