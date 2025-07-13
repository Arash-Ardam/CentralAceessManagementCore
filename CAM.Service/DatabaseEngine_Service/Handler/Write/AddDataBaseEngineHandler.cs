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
    public class AddDataBaseEngineHandler : IRequestHandler<AddDataBaseEngineCommand>
    {
        private readonly IDataBaseEngineRepo _writeRepo;
        private readonly IMediator _mediator;

        public AddDataBaseEngineHandler(IDataBaseEngineRepo writeRepo,IMediator mediator)
        {
            _writeRepo = writeRepo;
            _mediator = mediator;
        }

        public async Task Handle(AddDataBaseEngineCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.AddDataBaseEngine(request.dcName, request.name, request.address);

            await _mediator.Publish(new AddedDataBaseEngineEvent(request.dcName,request.name,request.address));
        }
    }
}
