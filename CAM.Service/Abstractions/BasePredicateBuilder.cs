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
    public class BasePredicateBuilder : IPredicateBuilder
    {
        public ExpressionStarter<DataCenter> GetDataCenterPredicate(SearchDCDto searchDto)
        {
            var predicate = PredicateBuilder.New<DataCenter>(true);

            predicate.And(x => x.Name == searchDto.DCSourceName);

            return predicate;
        }

        public  ExpressionStarter<DatabaseEngine> GetDbEnginePredicate(SearchDCDto searchDto)
        {
            var basePredicate = PredicateBuilder.New<DatabaseEngine>(true);

            if (searchDto.HasDbEngineName())
                basePredicate.Or(x => x.Name == searchDto.DBEngineName);

            if (searchDto.HasDbEngineAddess())
                basePredicate.Or(x => x.Address == searchDto.DBEngineAddress);

            return basePredicate;

        }

    }
}
