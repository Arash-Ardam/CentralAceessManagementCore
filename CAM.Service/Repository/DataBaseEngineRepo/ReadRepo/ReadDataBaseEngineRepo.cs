using CAM.Service.Dto;
using Domain.DataModels;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using ReadAppDbContext.DataModels;
using ReadAppDbContext.DbContexts;
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
        private readonly ReadTenantDbContext _readDbContext;
        private readonly DbSet<ReadDBEngine> _dbSet;

        public ReadDataBaseEngineRepo(ReadTenantDbContext readTenantDbContext)
        {
            _readDbContext = readTenantDbContext;
            _dbSet = readTenantDbContext.Set<ReadDBEngine>();
            _readDbContext.Database.EnsureCreated();
        }

        public async Task AddDataBaseEngine(string name, string address, string dcName)
        {
            await _dbSet.AddAsync(new ReadDBEngine { Name = name, Address = address,DataCenterName = dcName });
            await _readDbContext.SaveChangesAsync();
        }

        public async Task DeleteDataBaseEngine(string name, string dcName)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.DataCenterName == dcName && x.Name == name) ?? null;
            if (entity != null)
                _dbSet.Remove(entity);

            await _readDbContext.SaveChangesAsync();
        }

        public async Task<DatabaseEngine> GetDatabaseEngine(string dcName, string name)
        {
           var entity = await _dbSet.FirstOrDefaultAsync(x => x.DataCenterName == dcName && x.Name == name) ?? null;

            return entity switch
            {
                null => DatabaseEngine.Empty,
                _ => DatabaseEngine.CreateByNameAndAddress(entity.Name, entity.Address)
            };


        }

        public async Task<IEnumerable<DatabaseEngine>> SearchDataBaseEngine(SearchDCDto searchDCDto)
        {
            var predicate = PredicateBuilder.New<ReadDBEngine>(true);

            if(searchDCDto.HasSourceDCName())
                predicate.And(x => x.DataCenterName == searchDCDto.DCSourceName);

            if (searchDCDto.HasDbEngineName())
                predicate.And(x => x.Name == searchDCDto.DBEngineName);

            if (searchDCDto.HasDbEngineAddess())
                predicate.And(x => x.Address == searchDCDto.DBEngineAddress);

            IEnumerable<DatabaseEngine> entity = _dbSet.Where(predicate).Select(x => DatabaseEngine.CreateByNameAndAddress(x.Name,x.Address));

            return await Task.FromResult(entity);

        }
    }
}
