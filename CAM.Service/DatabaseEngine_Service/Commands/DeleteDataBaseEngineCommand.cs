using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Commands
{
    public record DeleteDataBaseEngineCommand(string dcName,string name) : IRequest
    {
    }
}
