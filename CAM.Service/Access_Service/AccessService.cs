using CAM.Service.Abstractions;
using CAM.Service.Access_Service.Command;
using CAM.Service.Access_Service.Queries;
using CAM.Service.Dto;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
using Domain.Enums;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service
{
    internal class AccessService : IAccessService
    {
        private readonly AccessValidator _validator;
        private readonly IMediator _mediator;

        public AccessService(AccessValidator validator,IMediator mediator)
        {
            _validator = validator;
            _mediator = mediator;
        }
        public async Task<Access> CreateAcceess(AccessBaseDto dto)
        {
            await _validator.ValidateEntries(dto);
            return await _mediator.Send(new CreateAccessCommand(dto));
        }

        public async Task<Access> GetAccess(short id)
        {
            return await _mediator.Send(new GetAccessQuery(id));
        }


        public async Task RemoveAccess(Access entry)
        {
            await _mediator.Send(new DeleteAccessCommand(entry));
        }


        public async Task<List<Access>> SearchAccess(AccessBaseDto dto)
        {
            return await _mediator.Send(new SearchAccessQuery(dto));
        }


        

    }
}
