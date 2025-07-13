using CAM.Service.DatabaseEngine_Service.Events;
using CAM.Service.Repository.DataBaseEngineRepo.ReadRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Handler.Sync
{
    public class DeletedDataBaseEngineEventHandler : INotificationHandler<DeletedDataBaseEngineEvent>
    {
        private readonly IReadDataBaseEngineRepo _readRepo;

        public DeletedDataBaseEngineEventHandler(IReadDataBaseEngineRepo readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task Handle(DeletedDataBaseEngineEvent notification, CancellationToken cancellationToken)
        {
            await _readRepo.DeleteDataBaseEngine(notification.name, notification.dcName);
        }
    }
}
