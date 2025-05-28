using CAM.Service.Dto;
using Domain.DataModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Abstractions
{
    public interface IPredicateBuilder
    {
        ExpressionStarter<DatabaseEngine> GetDbEnginePredicate(SearchDCDto searchDto);

        ExpressionStarter<DataCenter> GetDataCenterPredicate(SearchDCDto searchDto);
    }
}
