using CAM.Service.DataCenter_Service.Queries;
using CAM.Service.Repository.DataCenterRepo.ReadRepo;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service.Handlers.Read
{
    public class GetDataCenterWithDatabaseEnginesHandler : IRequestHandler<GetDataCenterWithDatabaseEnginesQuery, DataCenter>
    {
        private readonly IReadDataCenterRepository _readRepo;

        public GetDataCenterWithDatabaseEnginesHandler(IReadDataCenterRepository readDataCenterRepository)
        {
            _readRepo = readDataCenterRepository;
        }

        public async Task<DataCenter> Handle(GetDataCenterWithDatabaseEnginesQuery request, CancellationToken cancellationToken)
        {
            return await _readRepo.GetDataCenterWithDatabaseEngines(request.name);
        }
    }
}
