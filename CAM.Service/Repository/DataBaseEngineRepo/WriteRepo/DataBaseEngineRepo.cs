using CAM.Service.Abstractions;
using CAM.Service.Dto;
using CAM.Service.Repository.DataBaseEngineRepo.WriteRepo;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
using Domain.DataModels;
using Newtonsoft.Json;
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

        public DataBaseEngineRepo(ApplicationDbContext.ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddDataBaseEngine(string dcName, string dbEngineName, string address)
        {
            DataCenter dataCenter = _dbContext.DataCenters.FirstOrDefault(x => x.Name == dcName);

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


        public async Task RemoveDataBaseEngine(string dcName, string dbEngineName)
        {
            DataCenter dataCenter = _dbContext.DataCenters.FirstOrDefault(x => x.Name == dcName)
                ?? DataCenter.Empty;

            DatabaseEngine databaseEngine = _dbContext.Entry(dataCenter)
                .Collection(dc => dc.DatabaseEngines)
                .Query()
                .FirstOrDefault(dbE => dbE.Name == dbEngineName) ?? DatabaseEngine.Empty;

            DeleteRelatedAccesses(dataCenter, databaseEngine);

            DeleteDbEngine(dataCenter, databaseEngine);


            await SaveChangesAsync();
        }

        private void DeleteRelatedAccesses(DataCenter dataCenter, DatabaseEngine databaseEngine)
        {
            var relatedAccesses = _dbContext.Entry(dataCenter)
                .Collection(dc => dc.Accesses)
                .Query()
                .Where(
                    x => x.Source == JsonConvert.SerializeObject(databaseEngine)
                    ||
                    x.Destination == JsonConvert.SerializeObject(databaseEngine))
                .ToList();

            dataCenter.Accesses.Clear();

            _dbContext.Entry(dataCenter).Collection(dc => dc.Accesses).IsModified = true;
        }

        private void DeleteDbEngine(DataCenter dataCenter, DatabaseEngine dbEngineName)
        {
            dataCenter.DatabaseEngines.Remove(dbEngineName);

            _dbContext.Entry(dataCenter).Collection(dc => dc.DatabaseEngines).IsModified = true;
        }

        private async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
