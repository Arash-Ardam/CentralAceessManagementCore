using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service.Commands
{
    public record SyncDataCenterCommand(string name) : IRequest
    {
    }
}
