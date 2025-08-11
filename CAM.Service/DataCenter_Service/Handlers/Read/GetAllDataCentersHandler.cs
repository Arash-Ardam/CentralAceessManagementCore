using CAM.Service.DataCenter_Service.Queries;
using CAM.Service.Repository.DataCenterRepo.ReadRepo;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service.Handlers.Read
{
    public class GetAllDataCentersHandler : IRequestHandler<GetAllDataCentersQuery, List<DataCenter>>
    {
        private readonly IDataCenterSqlDataRepository _readRepo;

        public GetAllDataCentersHandler(IDataCenterSqlDataRepository readDataCenterRepository)
        {
            _readRepo = readDataCenterRepository;
        }

        public async Task<List<DataCenter>> Handle(GetAllDataCentersQuery request, CancellationToken cancellationToken)
        {
            return await _readRepo.GetAllDataCenters();
        }
    }
}
