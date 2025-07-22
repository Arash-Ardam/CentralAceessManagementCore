using CAM.Service.Abstractions;
using CAM.Service.Access_Service.Command;
using CAM.Service.Access_Service.Queries;
using CAM.Service.DatabaseEngine_Service.Queries;
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
        private readonly IMediator _mediator;

        public AccessService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Access> CreateAcceess(AccessBaseDto dto)
        {
            await ValidateEntries(dto);
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


        private async Task ValidateEntries(AccessBaseDto dto)
        {
            await ValidateAccessParams(dto);

            (dto.FromDCName, dto.FromName, dto.FromAddress) = await ValidateDbEngines(
                dto.FromDCName,
                dto.FromName,
                dto.FromAddress,
                "Source");

            (dto.ToDCName, dto.ToName, dto.ToAddress) = await ValidateDbEngines(
                dto.ToDCName,
                dto.ToName,
                dto.ToAddress,
                "Destination");
        }

        private async Task<(string dcName, string name, string address)> ValidateDbEngines(
            string dcName,
            string dbEngineName,
            string address,
            string side)
        {
            SearchDbEngineDto searchDto = new SearchDbEngineDto();

            searchDto.dcName = dcName;
            searchDto.dbEngineName = dbEngineName;
            searchDto.Address = address;
            var exsistedDbEngines = await _mediator.Send(new SearchDataBaseEngineQuery(searchDto));

            if (exsistedDbEngines.Count() == 0)
                throw new Exception($"No {side} DbEngine with name: {dbEngineName} Or address : {address} found");

            return (searchDto.dcName, exsistedDbEngines.First().Name, exsistedDbEngines.First().Address);
        }

        private async Task ValidateAccessParams(AccessBaseDto dto)
        {
            var existedAccess = await _mediator.Send(new SearchAccessQuery(dto));
            if (existedAccess.Count() != 0)
                throw new Exception("Access params are not correct");

        }

    }
}
