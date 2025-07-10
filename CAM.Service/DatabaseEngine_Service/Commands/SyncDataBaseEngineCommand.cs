using MediatR;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Commands
{
    public record SyncDataBaseEngineCommand(string name,string address ,string dcName) : IRequest
    {
    }
}
