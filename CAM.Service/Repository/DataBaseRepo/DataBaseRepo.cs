using CAM.Service.Dto;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataBaseRepo
{
    internal class DataBaseRepo : IDataBaseRepo
    {
        private readonly ApplicationDbContext.ApplicationDbContext _dbContext;
        private readonly IDataCenterSqlDataRepository _dataCenterRepo;

        public DataBaseRepo(ApplicationDbContext.ApplicationDbContext dbContext, IDataCenterSqlDataRepository dataCenterRepo)
        {
            _dbContext = dbContext;
            _dataCenterRepo = dataCenterRepo;
        }

        #region DataBase
        public async Task AddDataBaseToDataBaseEngine(string dcName, string dbEngineName, string dbName)
        {
            SearchDCDto searchDCDto = new SearchDCDto.Create()
                .AddDcName(dcName)
                .AddDbEngineName(dbEngineName)
                .Build();

            DataCenter dataCenter = await _dataCenterRepo.GetDataCenterWithParams(searchDCDto);

            if (dataCenter == default)
                throw new Exception();

            if (dataCenter.DatabaseEngines.Count == 0)
                throw new Exception();

            _dbContext.Entry(dataCenter.DatabaseEngines[0])
                      .Collection(e => e.Databases)
                      .Query()
                      .FirstOrDefault(x => x.Name == dbName);

            if (dataCenter.DatabaseEngines[0].Databases.Count != 0)
                throw new Exception();

            dataCenter.DatabaseEngines[0].AddDatabase(new Database() { Name = dbName });

            _dbContext.Entry(dataCenter).Collection(dc => dc.DatabaseEngines).IsLoaded = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Database>> SearchDataBase(string dcName, string dbEngineName, string dbName)
        {
            SearchDCDto searchDCDto = new SearchDCDto.Create()
                .AddDcName(dcName)
                .AddDbEngineName(dbEngineName)
                .Build();

            DataCenter dataCenter = await _dataCenterRepo.GetDataCenterWithParams(searchDCDto);

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
    }
}
