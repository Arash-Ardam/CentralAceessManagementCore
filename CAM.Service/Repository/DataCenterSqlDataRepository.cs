using ApplicationDbContext;
using CAM.Service.Dto;
using Domain.DataModels;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CAM.Service.Repository
{
    internal class DataCenterSqlDataRepository : IDataCenterSqlDataRepository
    {
        private readonly ApplicationDbContext.ApplicationDbContext _dbContext;

        public DataCenterSqlDataRepository(ApplicationDbContext.ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region DataCenter

        public async Task AddDataCenter(DataCenter dataCenter)
        {
            bool isExist = _dbContext.DataCenters.Any(dc => dc.Name == dataCenter.Name);

            if (isExist)
                throw new Exception();

            if (dataCenter != default)
            {
               await _dbContext.DataCenters.AddAsync(dataCenter);
               await SaveChangesAsync();
            }
            else
                throw new Exception();
        }

        public async Task<List<DataCenter>> GetAllDataCenters()
        {
            return await _dbContext.DataCenters.ToListAsync();
        }

        public async Task<DataCenter> GetDataCenter(string name)
        {
            return await _dbContext.DataCenters.FirstOrDefaultAsync(dc => dc.Name == name);
        }
        public async Task<DataCenter> GetDataCenterWithEngines(string name)
        {
            DataCenter dataCenter = await GetDataCenter(name);

            if (dataCenter != default) 
            {
                _dbContext.Entry(dataCenter)
                          .Collection(dc => dc.DatabaseEngines)
                          .Query()
                          .ToList();
                return dataCenter;
            }
            else throw new Exception();
        }

        public async Task<DataCenter> TryGetDataCenterWithAccessNames(string dataCenterName,string sourceName,string destinationName)
        {
            DataCenter dataCenter = await GetDataCenter(dataCenterName);
            if(dataCenter == default)
                return DataCenter.Empty;

            _dbContext.Entry(dataCenter)
                .Collection(dc => dc.DatabaseEngines)
                .Query()
                .Where(dbE => dbE.Name == sourceName || dbE.Name == destinationName)
                .ToList();

            return dataCenter;
        }

        public async Task<DataCenter> TryGetDataCenterWithGivenParams(string dcName, string dbEngineName, string address)
        {
            DataCenter dataCenter = await GetDataCenter(dcName);

            if (dataCenter != default)
            {
                _dbContext.Entry(dataCenter)
                    .Collection(dc => dc.DatabaseEngines)
                    .Query()
                    .FirstOrDefault(dcEngine => dcEngine.Name == dbEngineName || dcEngine.Address == address);
                    

                return dataCenter;
            }
            else throw new Exception();
        }

        public async Task UpdateDataCenter(string oldName, string newName)
        {
            DataCenter? dataCenter = _dbContext.DataCenters.FirstOrDefault(dc => dc.Name == oldName);

            if (dataCenter != default)
            {
                dataCenter.Name = newName;
                await SaveChangesAsync();
            }
            else throw new Exception();
        }

        public async Task DeleteDataCenter(string name)
        {
            DataCenter? existEntity = _dbContext.DataCenters.FirstOrDefault(dc => dc.Name == name);
            if (existEntity != default)
            {
                _dbContext.DataCenters.Remove(existEntity);
                await SaveChangesAsync();
            }
            else throw new Exception();
        }

        #endregion


        #region DataBaseEngine

        public async Task AddDataBaseEngine(string dcName, string dbEngineName, string address)
        {
            DataCenter dataCenter = await TryGetDataCenterWithGivenParams(dcName, dbEngineName, address);

            if (dataCenter.DatabaseEngines.Count != 0)
            {
                bool isDbEngineNameDuplicated = dataCenter.DatabaseEngines[0].Name == dbEngineName;
                bool isAddressDuplicated = dataCenter.DatabaseEngines[0].Address == address;

                if (isDbEngineNameDuplicated)
                    throw new Exception($"Entity with Param : {dbEngineName} Already Exist");
                if (isAddressDuplicated)
                    throw new Exception($"Entity with Param : {address} Already Exist");
            }

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
            DataCenter? dataCenter = await GetDataCenter(searchDto.dcName);

            var predicate = PredicateBuilder.New<DatabaseEngine>(true);
            if (!string.IsNullOrWhiteSpace(searchDto.dbEngineName))
            {
                predicate.Or(x => x.Name == searchDto.dbEngineName);
            }
            if (!string.IsNullOrWhiteSpace(searchDto.Address))
            {
                predicate.Or(x => x.Address == searchDto.Address);
            }

            if (dataCenter == default)
                return new List<DatabaseEngine>();

            if(dataCenter != default)
            {
                _dbContext.Entry(dataCenter)
                    .Collection(dc => dc.DatabaseEngines)
                    .Query()
                    .Where(predicate)
                    .ToList();
            }

            if(searchDto.withDatabases)
            {
                foreach(DatabaseEngine engine in dataCenter.DatabaseEngines)
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
            DataCenter dataCenter = await TryGetDataCenterWithGivenParams(dcName, dbEngineName,string.Empty);

            if(dataCenter==default)
            {
                throw new Exception();
            }

            else if(dataCenter.DatabaseEngines.Count == 0)
            {
                throw new Exception();
            }

            dataCenter.DatabaseEngines.Remove(dataCenter.DatabaseEngines[0]);

            _dbContext.Entry(dataCenter).Collection(dc => dc.DatabaseEngines).IsModified = true;

            await _dbContext.SaveChangesAsync();
        }

        #endregion


        #region DataBase
        public async Task AddDataBaseToDataBaseEngine(string dcName, string dbEngineName, string dbName)
        {
            DataCenter dataCenter = await TryGetDataCenterWithGivenParams(dcName, dbEngineName, string.Empty);

            if(dataCenter==default) 
                throw new Exception();

            if(dataCenter.DatabaseEngines.Count==0)
                throw new Exception();

            _dbContext.Entry(dataCenter.DatabaseEngines[0])
                      .Collection(e => e.Databases)
                      .Query()
                      .FirstOrDefault(x => x.Name == dbName);

            if (dataCenter.DatabaseEngines[0].Databases.Count != 0)
                throw new Exception();

            dataCenter.DatabaseEngines[0].AddDatabase(new Database() { Name = dbName});

            _dbContext.Entry(dataCenter).Collection(dc => dc.DatabaseEngines).IsLoaded = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Database>> SearchDataBase(string dcName, string dbEngineName, string dbName)
        {
            DataCenter dataCenter = await TryGetDataCenterWithGivenParams(dcName, dbEngineName, string.Empty);

            if (dataCenter == default)
                throw new Exception();

            if (dataCenter.DatabaseEngines.Count == 0)
                throw new Exception();

            var predicate = PredicateBuilder.New<Database>(true);
            if (!string.IsNullOrEmpty(dbName))
                predicate.And(db => db.Name == dbName);

            return _dbContext.Entry(dataCenter.DatabaseEngines[0])
                             .Collection(e => e.Databases)
                             .Query()
                             .Where(predicate)
                             .ToList();
        }

        #endregion

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


    }
}
