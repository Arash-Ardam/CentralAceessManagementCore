using CAM.Service.DataCenter_Service.Commands;
using CAM.Service.Repository.DataCenterRepo.SyncRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service.Handlers.Sync
{
    public class SyncDataCenterHandler : IRequestHandler<SyncDataCenterCommand>
    {
        private readonly IDataCenterSyncRepository _syncRepo;

        public SyncDataCenterHandler(IDataCenterSyncRepository syncRepo)
        {
            _syncRepo = syncRepo;
        }

        public async Task Handle(SyncDataCenterCommand request, CancellationToken cancellationToken)
        {
            await _syncRepo.SyncAsync(request.name);
        }
    }
}
