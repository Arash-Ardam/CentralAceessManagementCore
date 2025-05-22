using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository
{
    public interface IAccessRepository
    {
        Task<Access> CreateAccess(DataCenter dataCenter,Access access);
        Task<Access> RemoveAccess(string dataCenterName,Access access);
        Task<List<Access>> GetAllAccesses();
        Task<List<Access>> GetAllAccessesForDC(string dataCenterName);

        bool AnyAccessExist(DataCenter dataCenter, Access access, int port);

    }
}
