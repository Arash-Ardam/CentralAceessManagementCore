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
        Task<Access> CreateAcceess(AddAccessBaseDto dto);
    }
}
