using CAM.Service.Access_Service.Queries;
using CAM.Service.DatabaseEngine_Service.Commands;
using CAM.Service.DatabaseEngine_Service.Events;
using CAM.Service.DatabaseEngine_Service.Queries;
using CAM.Service.Repository.AccessRepo.WriteRepo;
using CAM.Service.Repository.DataBaseEngineRepo.WriteRepo;
using Domain.DataModels;
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
        private readonly IDataBaseEngineRepo _writeDbEngineRepo;
        private readonly IAccessRepository _writeAccessRepo;
        private readonly IMediator _mediator;

        public DeleteDataBaseEngineHandler(IDataBaseEngineRepo writedbEngineRepo,IAccessRepository writeAccessRepo,IMediator mediator)
        {
            _writeDbEngineRepo = writedbEngineRepo;
            _writeAccessRepo = writeAccessRepo;
            _mediator = mediator;
        }

        public async Task Handle(DeleteDataBaseEngineCommand request, CancellationToken cancellationToken)
        {
            await _writeDbEngineRepo.RemoveDataBaseEngine(request.dcName, request.name);

            await _mediator.Publish(new DeletedDataBaseEngineEvent(request.dcName, request.name));
        }
    }
}
