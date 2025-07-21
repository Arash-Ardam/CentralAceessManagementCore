using CAM.Service.Access_Service.Command;
using CAM.Service.Access_Service.Events;
using CAM.Service.Repository.AccessRepo.WriteRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service.Handlers.Write
{
    public class DeleteAccessCommandHandler : IRequestHandler<DeleteAccessCommand>
    {
        private IAccessRepository _writeRepo;
        private readonly IMediator _mediator;

        public DeleteAccessCommandHandler(IAccessRepository writeRepo, IMediator mediator)
        {
            _writeRepo = writeRepo;
            _mediator = mediator;
        }
        public async Task Handle(DeleteAccessCommand request, CancellationToken cancellationToken)
        {
            await _writeRepo.RemoveAccess(request.access);

            await _mediator.Publish(new DeletedAccessEvent(request.access.Source,request.access.Destination,request.access.Port));
        }
    }
}
