using CAM.Service.Dto;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service.Events
{
    public record CreatedAccessEvent(AccessBaseDto accessDto,DataCenter targetDC,Access validAccess) : INotification
    {
    }
}
