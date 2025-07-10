using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Commands
{
    public record AddDataBaseEngineCommand(string dcName,string name,string address) : IRequest
    {
    }
}
