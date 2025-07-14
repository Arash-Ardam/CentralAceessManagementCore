using CAM.Service.Dto;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service.Command
{
    public record CreateAccessCommand(AccessBaseDto accessDto) : IRequest<Access>
    {
    }
}
