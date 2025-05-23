using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.AccessRepo
{
    internal class AccessRepository : IAccessRepository
    {
        private readonly ApplicationDbContext.ApplicationDbContext _dbContext;
        public AccessRepository(ApplicationDbContext.ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Access> CreateAccess(DataCenter dataCenter, Access access)
        {

            dataCenter.AddAccess(access);
            _dbContext.Entry(dataCenter).Collection(x => x.Accesses).IsModified = true;


            await _dbContext.SaveChangesAsync();
            return access;
        }

        public bool AnyAccessExist(DataCenter dataCenter, Access access, int port)
        {
            return _dbContext
                .Entry(dataCenter)
                .Collection(dc => dc.Accesses)
                .Query()
                .Any(ac => ac.Source == access.Source && ac.Destination == access.Destination && ac.Port == port);
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
