using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository
{
    internal class AccessRepository : IAccessRepository
    {
        private readonly ApplicationDbContext.ApplicationDbContext _dbContext;
        public AccessRepository(ApplicationDbContext.ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Access> CreateAccess(string dataCenterName, Access access)
        {
            DataCenter dataCenter = _dbContext.DataCenters.FirstOrDefault(dc => dc.Name == dataCenterName) ?? DataCenter.Empty;
            if (dataCenter == DataCenter.Empty)
                throw new Exception();

            var existedAccess = _dbContext
                .Entry(dataCenter)
                .Collection(x => x.Accesses)
                .Query()
                .Contains(access);

            if(!existedAccess)
            {
                dataCenter.AddAccess(access);
                _dbContext.Entry(dataCenter).Collection(x=> x.Accesses).IsModified = true;
            }

            await _dbContext.SaveChangesAsync();
            return access;
        }

        public Task<Access> GetAccess(string dataCenterNames, Access access)
        {
            throw new NotImplementedException();
        }

        public Task<List<Access>> GetAllAccesses()
        {
            throw new NotImplementedException();
        }

        public Task<List<Access>> GetAllAccessesForDC(string dataCenterName)
        {
            throw new NotImplementedException();
        }

        public Task<Access> RemoveAccess(string dataCenterName, Access access)
        {
            throw new NotImplementedException();
        }
    }
}
