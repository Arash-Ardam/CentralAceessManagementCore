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

namespace CAM.Service.Repository.DataCenterRepo
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

        public async Task<DataCenter> GetDataCenterWithParams(SearchDCDto dto)
        {
            DataCenter dataCenter = DataCenter.Empty;
            var predicate = GetDbEnginePredicate(dto);

            if (dto.HasSourceDCName())
                dataCenter = await _dbContext.DataCenters.FirstOrDefaultAsync(dc => dc.Name == dto.DCSourceName) ?? dataCenter;

            if (dataCenter != DataCenter.Empty)
            {
                _dbContext.Entry(dataCenter)
                    .Collection(dc => dc.DatabaseEngines)
                    .Query()
                    .Where(predicate)
                    .ToList();
            }

            return dataCenter;

        }

        public async Task<(DataCenter source,DataCenter destination)> SearchSourceAndDestinationDataCenters(SearchDCDto dto)
        {
            DataCenter source = DataCenter.Empty;
            DataCenter destination = DataCenter.Empty;
            var predicate = GetDbEnginePredicate(dto);

            source = await _dbContext.DataCenters.FirstOrDefaultAsync(dc => dc.Name == dto.DCSourceName) ?? source;

            if (dto.DCSourceName != dto.DCDestinationName)
                destination = await _dbContext.DataCenters.FirstOrDefaultAsync(dc => dc.Name == dto.DCDestinationName) ?? destination;

            if (source != DataCenter.Empty)
            {
                _dbContext.Entry(source)
                    .Collection(dc => dc.DatabaseEngines)
                    .Query()
                    .Where(predicate)
                    .ToList();
            }

            if (destination != DataCenter.Empty)
            {
                _dbContext.Entry(destination)
                    .Collection(dc => dc.DatabaseEngines)
                    .Query()
                    .Where(predicate)
                    .ToList();
            }

            if (dto.DCSourceName == dto.DCDestinationName)
                return (source, source);

            return (source,destination);
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

        private ExpressionStarter<DatabaseEngine> GetDbEnginePredicate(SearchDCDto dCDto)
        {
            var dbEnginePredicateBuilder = PredicateBuilder.New<DatabaseEngine>(true);

            if (dCDto.HasAccessSourceName())
                dbEnginePredicateBuilder.Or(x => x.Name == dCDto.DCAccessSourceName);

            if (dCDto.HasAccessDestinationName())
                dbEnginePredicateBuilder.Or(x => x.Name == dCDto.DCAccessDestinationName);

            if (dCDto.HasAccessSourceAddress())
                dbEnginePredicateBuilder.Or(x => x.Address == dCDto.DCAccessSourceAddress);

            if (dCDto.HasAccessDestinationAddress())
                dbEnginePredicateBuilder.Or(x => x.Address == dCDto.DCAccessDestinationAddress);

            if (dCDto.HasDbEngineName())
                dbEnginePredicateBuilder.Or(x => x.Name == dCDto.DBEngineName);

            if (dCDto.HasDbEngineAddess())
                dbEnginePredicateBuilder.Or(x => x.Address == dCDto.DBEngineAddress);

            return dbEnginePredicateBuilder;
        }

        #endregion



        

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


    }
}
