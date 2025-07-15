using CAM.Service.Dto;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.AccessRepo.WriteRepo
{
    public interface IAccessRepository
    {
        Task<Access> CreateAccess(AccessBaseDto dto);
        Task RemoveAccess(Access access);
        Task RemoveRangeOfAccesses(List<Access> accessList);
        Access? GetAccess(short id);
        List<Access> GetRangeAccessByDbEngine(string jsonDbEngine);
        List<Access> SearchAccess(SearchAccessBaseDto searchAccessDto);

        bool AnyAccessExist(DataCenter dataCenter, Access access, int port);

    }
}
