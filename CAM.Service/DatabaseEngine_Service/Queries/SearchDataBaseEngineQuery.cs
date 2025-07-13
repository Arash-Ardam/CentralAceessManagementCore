using CAM.Service.Dto;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Queries
{
    public record SearchDataBaseEngineQuery(SearchDbEngineDto Dto) : IRequest<List<DatabaseEngine>>
    {
    }
}
