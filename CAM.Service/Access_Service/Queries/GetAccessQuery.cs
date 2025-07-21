using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service.Queries
{
    public record GetAccessQuery(short id) : IRequest<Access>
    {
    }
}
