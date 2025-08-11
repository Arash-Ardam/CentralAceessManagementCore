using ApplicationDbContext;
using ApplicationDbContext.DbContexts;
using ApplicationDbContext.EntityConfigs;
using ApplicationDbContext.Migrations;
using CAM.Service.Abstractions;
using CAM.Service.Dto;
using Domain.DataModels;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadAppDbContext.Configs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CAM.Service.Repository.DataCenterRepo.WriteRepo
{
    internal class DataCenterSqlDataRepository : IDataCenterSqlDataRepository
    {
        private readonly AdminDbContext _dbContext;
        private readonly DbSet<DataCenter> _dbSet;


        public DataCenterSqlDataRepository(AdminDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
            _dbSet = _dbContext.Set<DataCenter>();
            _dbContext.Database.EnsureCreated();
        }


        #region DataCenter

        public async Task AddDataCenter(DataCenter dataCenter)
        {
            bool isExist = _dbSet.Any(dc => dc.Name == dataCenter.Name);

            if (isExist)
                throw new Exception();

            if (dataCenter != default)
            {
                await _dbSet.AddAsync(dataCenter);
                await SaveChangesAsync();
            }
            else
                throw new Exception();
        }

        public async Task<List<DataCenter>> GetAllDataCenters()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<DataCenter> GetDataCenter(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(dc => dc.Name == name) ?? DataCenter.Empty;
        }


        public async Task<DataCenter> SearchDataCenter<TPredicator>(SearchDCDto dto) where TPredicator : IPredicateBuilder, new()
        {
            DataCenter dataCenter = DataCenter.Empty;
            var predicateBuilder = new TPredicator();

            var dcPredicate = predicateBuilder.GetDataCenterPredicate(dto);
            var dbEnginePredicate = predicateBuilder.GetDbEnginePredicate(dto);

            dataCenter = await _dbSet.FirstOrDefaultAsync(dcPredicate) ?? dataCenter;

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
            DataCenter? dataCenter = _dbSet.FirstOrDefault(dc => dc.Name == oldName);

            if (dataCenter != default)
            {
                dataCenter.Name = newName;
                await SaveChangesAsync();
            }
            else throw new Exception();
        }

        public async Task DeleteDataCenter(string name)
        {
            DataCenter? existEntity = _dbSet.FirstOrDefault(dc => dc.Name == name);
            if (existEntity != default)
            {
                _dbSet.Remove(existEntity);
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
