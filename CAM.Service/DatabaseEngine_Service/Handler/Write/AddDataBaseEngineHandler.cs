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
    public class AddDataBaseEngineHandler : IRequestHandler<AddDataBaseEngineCommand>
    {
        private readonly IDataBaseEngineRepo _writeRepo;

        public AddDataBaseEngineHandler(IDataBaseEngineRepo writeRepo)
        {
            _writeRepo = writeRepo;
        }

        public async Task Handle(AddDataBaseEngineCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.AddDataBaseEngine(request.dcName, request.name, request.address);
        }
    }
}
