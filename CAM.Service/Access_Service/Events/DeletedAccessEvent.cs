using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service.Events
{
    public record DeletedAccessEvent(string source,string destination) : INotification
    {
    }
}
