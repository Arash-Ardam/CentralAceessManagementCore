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
    public class DestinationPredicateBuilder : IPredicateBuilder
    {
        public ExpressionStarter<DataCenter> GetDataCenterPredicate(SearchDCDto searchDto)
        {
            var predicate = PredicateBuilder.New<DataCenter>(true);

            predicate.And(x => x.Name == searchDto.DCDestinationName);

            return predicate;
        }

        public ExpressionStarter<DatabaseEngine> GetDbEnginePredicate(SearchDCDto searchDto)
        {
            var destinationPredicate = PredicateBuilder.New<DatabaseEngine>(true);

            if (searchDto.HasAccessDestinationName())
                destinationPredicate.Or(x => x.Name == searchDto.DCAccessDestinationName);

            if (searchDto.HasDbEngineAddess())
                destinationPredicate.Or(x => x.Address == searchDto.DCAccessDestinationAddress);


            return destinationPredicate;
        }

    }
}
