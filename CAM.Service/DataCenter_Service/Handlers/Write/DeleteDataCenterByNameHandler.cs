using CAM.Service.DataCenter_Service.Commands;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service.Handlers.Write
{
    public class DeleteDataCenterByNameHandler : IRequestHandler<DeleteDataCenterByNameCommand>
    {
        private readonly IDataCenterSqlDataRepository _writeRepo;

        public DeleteDataCenterByNameHandler(IDataCenterSqlDataRepository writeRepo)
        {
            _writeRepo = writeRepo;
        }

        public async Task Handle(DeleteDataCenterByNameCommand request, CancellationToken cancellationToken)
        {
           await _writeRepo.DeleteDataCenter(request.name);
        }
    }
}
