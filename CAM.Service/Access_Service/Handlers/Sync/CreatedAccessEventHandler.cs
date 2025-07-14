using CAM.Service.Access_Service.Events;
using CAM.Service.Repository.AccessRepo.ReadRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service.Handlers.Sync
{
    public class CreatedAccessEventHandler : INotificationHandler<CreatedAccessEvent>
    {
        private readonly IReadAccessRepository _readRepo;

        public CreatedAccessEventHandler(IReadAccessRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task Handle(CreatedAccessEvent notification, CancellationToken cancellationToken)
        {
            await _readRepo.CreateAccess(notification.accessDto,notification.targetDC,notification.validAccess);
        }
    }
}
