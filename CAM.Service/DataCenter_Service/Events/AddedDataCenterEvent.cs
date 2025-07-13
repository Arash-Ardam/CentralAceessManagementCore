using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service.Events
{
    public record AddedDataCenterEvent(string name) : INotification
    {
    }
}
