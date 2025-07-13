using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Events
{
    public record AddedDataBaseEngineEvent(string dcName,string name,string address) : INotification
    {
    }
}
