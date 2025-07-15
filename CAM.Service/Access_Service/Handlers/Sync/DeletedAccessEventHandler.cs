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
    public class DeletedAccessEventHandler : INotificationHandler<DeletedAccessEvent>
    {
        private readonly IReadAccessRepository _readRepo;

        public DeletedAccessEventHandler(IReadAccessRepository readRepo)
        {
            _readRepo = readRepo;    
        }

        public async Task Handle(DeletedAccessEvent notification, CancellationToken cancellationToken)
        {
            await _readRepo.DeleteAccess(notification.source, notification.destination);
        }
    }
}
