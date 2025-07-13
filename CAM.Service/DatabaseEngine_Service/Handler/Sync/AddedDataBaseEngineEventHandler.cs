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
    public class AddedDataBaseEngineEventHandler : INotificationHandler<AddedDataBaseEngineEvent>
    {
        private readonly IReadDataBaseEngineRepo _readRepo;

        public AddedDataBaseEngineEventHandler(IReadDataBaseEngineRepo readRepo)
        {
            _readRepo = readRepo;
        }
        public async Task Handle(AddedDataBaseEngineEvent notification, CancellationToken cancellationToken)
        {
            await _readRepo.AddDataBaseEngine(notification.name, notification.address, notification.dcName);
        }
    }
}
