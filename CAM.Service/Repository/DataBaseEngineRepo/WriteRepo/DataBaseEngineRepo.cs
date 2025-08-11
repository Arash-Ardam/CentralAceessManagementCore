using ApplicationDbContext.DbContexts;
using CAM.Service.Abstractions;
using CAM.Service.Dto;
using CAM.Service.Repository.DataBaseEngineRepo.WriteRepo;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;
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
        private readonly WriteTenantDbContext _dbContext;
        private readonly DbSet<DatabaseEngine> _dbSet;

        public DataBaseEngineRepo(WriteTenantDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<DatabaseEngine>();
            _dbContext.Database.EnsureCreated();
        }

        public async Task AddDataBaseEngine(string dcName, string dbEngineName, string address)
        {
            await _dbSet.AddAsync(DatabaseEngine.CreateByNameAndAddress(dbEngineName, address));
            

            await SaveChangesAsync();

        }

        public Task<DatabaseEngine> GetDataBaseEngine(string name)
        {
            throw new NotImplementedException();
        }


        public async Task RemoveDataBaseEngine(string dcName, string dbEngineName)
        {
            var entity = _dbSet.FirstOrDefault(x => x.Name == dbEngineName) ?? DatabaseEngine.Empty;

            if(entity == DatabaseEngine.Empty)
                throw new ArgumentNullException($"no dbEngine with name {dbEngineName} exists");

            _dbSet.Remove(entity);
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
