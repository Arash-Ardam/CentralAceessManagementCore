using CAM.Service.DatabaseEngine_Service.Commands;
using CAM.Service.Repository.DataBaseEngineRepo.SyncRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Handler.Sync
{
    public class SyncDatabaseEngineHandler : IRequestHandler<SyncDataBaseEngineCommand>
    {
        private readonly ISyncDatabaseEngineRepo _syncRepo;

        public SyncDatabaseEngineHandler(ISyncDatabaseEngineRepo syncRepo)
        {
            _syncRepo = syncRepo;
        }

        public async Task Handle(SyncDataBaseEngineCommand request, CancellationToken cancellationToken)
        {
            await _syncRepo.SyncDatabaseEngine(request.dcName, request.name, request.address);
        }
    }
}
