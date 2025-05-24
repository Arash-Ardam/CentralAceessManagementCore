using CAM.Service.Dto;
using CAM.Service.Dtos;
using CAM.Service.Repository.AccessRepo;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
using Domain.Enums;
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
        private readonly IDataCenterSqlDataRepository _dataCenterSqlRepo;
        private readonly IAccessRepository _accessRepo;

        public AccessService(IDataCenterSqlDataRepository dataCenterSqlRepo, IAccessRepository accessRepo)
        {
            _dataCenterSqlRepo = dataCenterSqlRepo;
            _accessRepo = accessRepo;
        }
        public async Task<Access> CreateAcceess(AddAccessByNameDto dto)
        {
            var validatedEntries = await GetValidatedEntries(dto);
            bool isAccessExist = _accessRepo.AnyAccessExist(validatedEntries.dataCenter, validatedEntries.access, dto.Port);

            if (isAccessExist)
                throw new Exception("Alredy Exist");

           return  await _accessRepo.CreateAccess(validatedEntries.dataCenter, validatedEntries.access);
        }

       

        private async Task<(DataCenter dataCenter , Access access)> GetValidatedEntries(AddAccessByNameDto dto) 
        {
            var validatedDCs = await GetValidatedDataCenters(dto);

            var TargetDbEngines = HasValidatedDbEngines(validatedDCs, dto);
            if (!TargetDbEngines.hasSource)
                throw new Exception($"No source DbEngine with name: {dto.FromName} found");
            if (!TargetDbEngines.hasDestination)
                throw new Exception($"No destination DbEngine with name: {dto.ToName} found");

            var validatedAccess = GetValidatedAccess(validatedDCs,dto);

            return(validatedDCs.sourceDC, validatedAccess);
        }

        private async Task<(DataCenter sourceDC, DataCenter destinationDC)> GetValidatedDataCenters(AddAccessByNameDto dto)
        {
            var searchDto = new SearchDCDto.Create()
                    .AddSourceDcName(dto.FromDCName)
                    .AddDestinationDcName(dto.ToDCName)
                    .AddAccessSourceName(dto.FromName)
                    .AddAccessDestinationName(dto.ToName)
                    .Build();

            var searchedDcs = await _dataCenterSqlRepo.SearchSourceAndDestinationDataCenters(searchDto);

            if (searchedDcs.source == DataCenter.Empty)
                throw new Exception($"No DataCenter with name: {dto.FromDCName} found");

            if (searchedDcs.destination == DataCenter.Empty)
                throw new Exception($"No DataCenter with name: {dto.ToDCName} found");

            return searchedDcs;
        }

        private (bool hasSource,bool hasDestination)
            HasValidatedDbEngines((DataCenter sourceDC, DataCenter destinationDC) validatedDCs,AddAccessByNameDto dto)
        {
            bool hasSource = false;
            bool hasDestination = false;

            if (validatedDCs.sourceDC.DatabaseEngines.Any(x => x.Name == dto.FromName))
                hasSource = true;

            if (validatedDCs.destinationDC.DatabaseEngines.Any(x => x.Name == dto.ToName))
                hasDestination = true;

            return (hasSource,hasDestination);
        }

        private Access GetValidatedAccess((DataCenter sourceDC, DataCenter destinationDC) targetDCs, AddAccessByNameDto dto)
        {
            string serializedSource = string.Empty;
            string serializedDestination = string.Empty;

            if (dto.Direction == DatabaseDirection.InBound)
            {
                serializedSource = JsonConvert.SerializeObject(targetDCs.sourceDC.DatabaseEngines[0]);
                serializedDestination = JsonConvert.SerializeObject(targetDCs.sourceDC.DatabaseEngines[1]);
            }
            else
            {
                serializedSource = JsonConvert.SerializeObject(targetDCs.sourceDC.DatabaseEngines[0]);
                serializedDestination = JsonConvert.SerializeObject(targetDCs.destinationDC.DatabaseEngines[0]);
            }

            Access validatedAccess = new Access.Create()
                .AddSource(serializedSource)
                .AddPort(dto.Port)
                .AddDestination(serializedDestination)
                .SetDirection(dto.Direction)
                .Build();

            return validatedAccess;
        }

    }
}
