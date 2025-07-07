using CAM.Service.DataCenter_Service.Commands;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service.Handlers.Write
{
    public class AddDataCenterByNameHandler : IRequestHandler<AddDataCenterByNameCommand>
    {
        private IDataCenterSqlDataRepository _writeRepo;

        public AddDataCenterByNameHandler(IDataCenterSqlDataRepository dataCenterSqlDataRepository)
        {
            _writeRepo = dataCenterSqlDataRepository;
        }

        public async Task Handle(AddDataCenterByNameCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.AddDataCenter(DataCenter.CreateByName(request.name));
        }
    }
}
