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
    public class SourcePredicateBuilder : IPredicateBuilder
    {
        public ExpressionStarter<DataCenter> GetDataCenterPredicate(SearchDCDto searchDto)
        {
            var predicate = PredicateBuilder.New<DataCenter>(true);

            predicate.And(x => x.Name == searchDto.DCSourceName);

            return predicate;
        }

        public ExpressionStarter<DatabaseEngine> GetDbEnginePredicate(SearchDCDto searchDto)
        {
            var sourcePredicate = PredicateBuilder.New<DatabaseEngine>(true);

            if (searchDto.HasAccessSourceName())
                sourcePredicate.Or(x => x.Name == searchDto.DCAccessSourceName);

            if (searchDto.HasAccessSourceAddress())
                sourcePredicate.Or(x => x.Address == searchDto.DCAccessSourceAddress);

            return sourcePredicate;
        }


    }
}
