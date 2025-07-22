using CAM.Service.DatabaseEngine_Service.Events;
using CAM.Service.Repository.AccessRepo.ReadRepo;
using CAM.Service.Repository.AccessRepo.WriteRepo;
using CAM.Service.Repository.DataBaseEngineRepo.ReadRepo;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Handler.Sync
{
    public class DeletedDataBaseEngineEventHandler : INotificationHandler<DeletedDataBaseEngineEvent>
    {
        private readonly IReadDataBaseEngineRepo _readDbEngineRepo;
        private readonly IReadAccessRepository _readAccessRepo;

        public DeletedDataBaseEngineEventHandler(IReadDataBaseEngineRepo readDbEngineRepo,IReadAccessRepository readAccessRepo)
        {
            _readDbEngineRepo = readDbEngineRepo;
            _readAccessRepo = readAccessRepo;
        }

        public async Task Handle(DeletedDataBaseEngineEvent notification, CancellationToken cancellationToken)
        {
            await _readDbEngineRepo.DeleteDataBaseEngine(notification.name, notification.dcName);

            await _readAccessRepo.DeleteByRelatedDbEngine(notification.dcName, notification.name);
        }
    }
}
