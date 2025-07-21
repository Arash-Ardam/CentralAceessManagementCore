using CAM.Service.Access_Service.Queries;
using CAM.Service.DatabaseEngine_Service.Queries;
using CAM.Service.Dto;
using CAM.Service.Repository.AccessRepo;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Abstractions
{
    public class AccessValidator
    {
        private readonly IRepoUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public AccessValidator(IRepoUnitOfWork unitOfWork,IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task ValidateEntries(AccessBaseDto dto)
        {
            await ValidateAccessParams(dto);

            (dto.FromDCName,dto.FromName ,dto.FromAddress) = await ValidateDbEngines(
                dto.FromDCName,
                dto.FromName,
                dto.FromAddress,
                "Source");

            (dto.ToDCName,dto.ToName, dto.ToAddress) = await ValidateDbEngines(
                dto.ToDCName,
                dto.ToName,
                dto.ToAddress,
                "Destination");
        }

        private async Task<(string dcName,string name,string address)> ValidateDbEngines(
            string dcName,
            string dbEngineName,
            string address,
            string side)
        {
            SearchDbEngineDto searchDto = new SearchDbEngineDto();

            searchDto.dcName = dcName;
            searchDto.dbEngineName = dbEngineName;
            searchDto.Address =  address;
            var exsistedDbEngines = await _mediator.Send(new SearchDataBaseEngineQuery(searchDto));

            if (exsistedDbEngines.Count() == 0)
                throw new Exception($"No {side} DbEngine with name: {dbEngineName} Or address : {address} found");

            return (searchDto.dcName,exsistedDbEngines.First().Name, exsistedDbEngines.First().Address);
        }

        private async Task ValidateAccessParams(AccessBaseDto dto) 
        {
            var existedAccess = await _mediator.Send(new SearchAccessQuery(dto));
            if (existedAccess.Count() != 0)
                throw new Exception("Access params are not correct");

        }

        public async Task<SearchAccessBaseDto> GetValidatedSearchEntry(AccessBaseDto dto)
        {
            var validatedEntries = await GetValidatedEntries(dto);
            var searchAccessDto = new SearchAccessBaseDto()
            {
                TargetDataCenter = validatedEntries.Source,

                SourceDCName = validatedEntries.Source.Name,
                DestinationDCName = validatedEntries.Destination.Name,

                Source = validatedEntries.ValidatedAccess.Source,
                SourceDbEs = validatedEntries.Source.DatabaseEngines.Select(x => JsonConvert.SerializeObject(x)).ToList(),

                Port = validatedEntries.ValidatedAccess.Port,

                Destination = validatedEntries.ValidatedAccess.Destination,
                DestinationDbEs = validatedEntries.Destination.DatabaseEngines.Select(x => JsonConvert.SerializeObject(x)).ToList(),

                Direction = validatedEntries.ValidatedAccess.Direction
            };

            return searchAccessDto;
        }

        public async Task<ValidatedDataEntry> GetValidatedEntries(AccessBaseDto dto)
        {
            var searchedDCs = await GetValidatedDataCenters(dto);

            var TargetDbEngines = GetValidatedDbEngines(searchedDCs.sourceDC, searchedDCs.destinationDC, dto);

            var validatedAccess = GetValidatedAccess(TargetDbEngines.source, TargetDbEngines.destination, dto);

            return new ValidatedDataEntry()
            {
                Source = searchedDCs.sourceDC,
                Destination = searchedDCs.destinationDC,
                ValidatedAccess = validatedAccess
            };
        }

        private async Task<(DataCenter sourceDC, DataCenter destinationDC)> GetValidatedDataCenters(AccessBaseDto dto)
        {
            var searchDto = new SearchDCDto.Create()
                    .AddSourceDcName(dto.FromDCName)
                    .AddDestinationDcName(dto.ToDCName)
                    .AddAccessSourceName(dto.FromName)
                    .AddAccessDestinationName(dto.ToName)
                    .AddAccessSourceAddress(dto.FromAddress)
                    .AddAccessDestinationAddress(dto.ToAddress)
                    .Build();

            var sourceDc = await _unitOfWork.DataCenterRepo.SearchDataCenter<SourcePredicateBuilder>(searchDto);
            var destinationDc = await _unitOfWork.DataCenterRepo.SearchDataCenter<DestinationPredicateBuilder>(searchDto);

            //if (sourceDc == DataCenter.Empty)
            //    throw new Exception($"No DataCenter with name: {dto.FromDCName} found");

            //if (destinationDc == DataCenter.Empty)
            //    throw new Exception($"No DataCenter with name: {dto.ToDCName} found");

            return (sourceDc,destinationDc);
        }

        private (DatabaseEngine source, DatabaseEngine destination)
            GetValidatedDbEngines(DataCenter sourceDC, DataCenter destinationDC, AccessBaseDto dto)
        {
            DatabaseEngine source = sourceDC
                .DatabaseEngines
                .FirstOrDefault(x => x.Name == dto.FromName || x.Address == dto.FromAddress) ?? DatabaseEngine.Empty;

            DatabaseEngine destination = destinationDC
                .DatabaseEngines
                .FirstOrDefault(x => x.Name == dto.ToName || x.Address == dto.ToAddress) ?? DatabaseEngine.Empty;

            if (dto.FromName != string.Empty || dto.FromAddress != string.Empty)
            {
                if (source == DatabaseEngine.Empty)
                    throw new Exception($"No source DbEngine with name: {dto.FromName} Or address : {dto.FromAddress} found");
            }
            if (dto.ToName != string.Empty || dto.ToAddress != string.Empty)
            {
                if (destination == DatabaseEngine.Empty)
                    throw new Exception($"No destination DbEngine with name: {dto.ToName} Or address : {dto.ToAddress} found");
            }

            return (source, destination);
        }

        private Access GetValidatedAccess(DatabaseEngine source, DatabaseEngine destination, AccessBaseDto dto)
        {
            return new Access.Create()
                .AddSource(source)
                .AddPort(dto.Port)
                .AddDestination(destination)
                .SetDirection(dto.Direction)
                .Build();
        }


    }
}
