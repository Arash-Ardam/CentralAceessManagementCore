using CAM.Service.DatabaseEngine_Service.Commands;
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

        public DeleteDataBaseEngineHandler(IDataBaseEngineRepo writeRepo)
        {
            _writeRepo = writeRepo;
        }

        public async Task Handle(DeleteDataBaseEngineCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.RemoveDataBaseEngine(request.dcName, request.name);
        }
    }
}
