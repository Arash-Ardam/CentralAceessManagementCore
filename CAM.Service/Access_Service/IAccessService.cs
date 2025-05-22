using CAM.Service.Dtos;
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
        Task<Access> CreateAccess(AddAccessByNameDto dto);
    }
}
