using CAM.Service.DataCenter_Service.Events;
using CAM.Service.Repository.DataCenterRepo.ReadRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service.Handlers.Sync
{
    public class DeletedDataCenterEventHandler : INotificationHandler<DeletedDataCenterEvent>
    {
        private readonly IReadDataCenterRepository _readRepo;

        public DeletedDataCenterEventHandler(IReadDataCenterRepository readRepo)
        {
            _readRepo = readRepo;    
        }

        public async Task Handle(DeletedDataCenterEvent notification, CancellationToken cancellationToken)
        {
            await _readRepo.DeleteDataCenter(notification.name);
        }
    }
}
