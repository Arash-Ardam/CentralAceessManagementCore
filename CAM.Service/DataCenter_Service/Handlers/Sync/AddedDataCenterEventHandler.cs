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
    public class AddedDataCenterEventHandler : INotificationHandler<AddedDataCenterEvent>
    {
        private IReadDataCenterRepository _readRepo;

        public AddedDataCenterEventHandler(IReadDataCenterRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task Handle(AddedDataCenterEvent notification, CancellationToken cancellationToken)
        {
            await _readRepo.AddDataCenter(notification.name);
        }
    }
}
