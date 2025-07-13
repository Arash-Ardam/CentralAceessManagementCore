using CAM.Service.DatabaseEngine_Service.Commands;
using CAM.Service.DatabaseEngine_Service.Events;
using CAM.Service.Repository.DataBaseEngineRepo.WriteRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Handler.Write
{
    public class DeleteDataBaseEngineHandler : IRequestHandler<DeleteDataBaseEngineCommand>
    {
        private readonly IDataBaseEngineRepo _writeRepo;
        private readonly IMediator _mediator;

        public DeleteDataBaseEngineHandler(IDataBaseEngineRepo writeRepo,IMediator mediator)
        {
            _writeRepo = writeRepo;
            _mediator = mediator;
        }

        public async Task Handle(DeleteDataBaseEngineCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.RemoveDataBaseEngine(request.dcName, request.name);

            await _mediator.Publish(new DeletedDataBaseEngineEvent(request.dcName, request.name));
        }
    }
}
