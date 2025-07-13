using ApplicationDbContext;
using ApplicationDbContext.Migrations;
using CAM.Service.Abstractions;
using CAM.Service.Dto;
using Domain.DataModels;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CAM.Service.Repository.DataCenterRepo.WriteRepo
{
    internal class DataCenterSqlDataRepository : IDataCenterSqlDataRepository
    {
        private readonly ApplicationDbContext.ApplicationDbContext _dbContext;

        public DataCenterSqlDataRepository(ApplicationDbContext.ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _dbContext = applicationDbContext;
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


        public async Task<DataCenter> SearchDataCenter<TPredicator>(SearchDCDto dto) where TPredicator : IPredicateBuilder, new()
        {
            DataCenter dataCenter = DataCenter.Empty;
            var predicateBuilder = new TPredicator();

            var dcPredicate = predicateBuilder.GetDataCenterPredicate(dto);
            var dbEnginePredicate = predicateBuilder.GetDbEnginePredicate(dto);

            dataCenter = await _dbContext.DataCenters.FirstOrDefaultAsync(dcPredicate) ?? dataCenter;

            if (dataCenter != DataCenter.Empty)
            {
                _dbContext.Entry(dataCenter)
                    .Collection(dc => dc.DatabaseEngines)
                    .Query()
                    .Where(dbEnginePredicate)
                    .ToList();
            }

            return dataCenter;
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
        private async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }


    }
}
