using CAM.Service.DataCenter_Service.Commands;
using CAM.Service.DataCenter_Service.Events;
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
        private readonly IMediator _mediator;

        public DeleteDataCenterByNameHandler(IDataCenterSqlDataRepository writeRepo,IMediator mediator)
        {
            _writeRepo = writeRepo;
            _mediator = mediator;
        }

        public async Task Handle(DeleteDataCenterByNameCommand request, CancellationToken cancellationToken)
        {
           await _writeRepo.DeleteDataCenter(request.name);

           //await _mediator.Publish(new DeletedDataCenterEvent(request.name));
        }
    }
}
