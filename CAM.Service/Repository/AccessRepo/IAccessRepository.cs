using CAM.Service.Dto;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.AccessRepo
{
    public interface IAccessRepository
    {
        Task<Access> CreateAccess(DataCenter dataCenter, Access access);
        Task<Access> RemoveAccess(string dataCenterName, Access access);
        Access? GetAccess(short id); 
        List<Access> SearchAccess(SearchAccessBaseDto searchAccessDto);

        bool AnyAccessExist(DataCenter dataCenter, Access access, int port);

    }
}
