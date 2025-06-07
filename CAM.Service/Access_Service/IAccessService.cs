using CAM.Service.Dto;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service
{
    public interface IAccessService
    {
        Task<Access> CreateAcceess(AccessBaseDto dto);
        Task<List<Access>> SearchAccess(AccessBaseDto dto);

        Task<Access> GetAccess(short id);
        List<Access> GetAccessesByDbEngine(DatabaseEngine databaseEngine);
        Task RemoveAccess(Access entry);   

        Task RemoveAccessInRange(List<Access> accessList);
    }
}
