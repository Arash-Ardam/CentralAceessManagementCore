using CAM.Service.Access_Service.Command;
using CAM.Service.Access_Service.Events;
using CAM.Service.Repository.AccessRepo.WriteRepo;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service.Handlers.Write
{
    public class CreateAccessCommandHandler : IRequestHandler<CreateAccessCommand, Access>
    {
        private readonly IAccessRepository _writeRepo;
        private readonly IMediator _mediator;

        public CreateAccessCommandHandler(IAccessRepository writeRepo,IMediator mediator)
        {
            _writeRepo = writeRepo;
            _mediator = mediator;
        }

        public async Task<Access> Handle(CreateAccessCommand request, CancellationToken cancellationToken)
        {

            DataCenter targetDC = DataCenter.CreateByName(request.accessDto.FromDCName);

            Access entryAccess = new Access.Create()
                .AddSource(DatabaseEngine.CreateByNameAndAddress(request.accessDto.FromName, request.accessDto.FromAddress))
                .AddDestination(DatabaseEngine.CreateByNameAndAddress(request.accessDto.ToName, request.accessDto.ToAddress))
                .AddPort(request.accessDto.Port)
                .SetDirection(request.accessDto.Direction)
                .Build();


            var result = await _writeRepo.CreateAccess(targetDC, entryAccess);

            if (result != Access.Empty)
                await _mediator.Publish(new CreatedAccessEvent(request.accessDto, targetDC, entryAccess));

            return result;

        }
    }
}
