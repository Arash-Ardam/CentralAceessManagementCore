using CAM.Service.Dto;
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
        public async Task<Access> CreateAcceess(AddAccessBaseDto dto)
        {
            var validatedEntries = await GetValidatedEntries(dto);
            bool isAccessExist = _accessRepo.AnyAccessExist(validatedEntries.dataCenter, validatedEntries.access, dto.Port);

            if (isAccessExist)
                throw new Exception("Alredy Exist");

           return  await _accessRepo.CreateAccess(validatedEntries.dataCenter, validatedEntries.access);
        }


        private async Task<(DataCenter dataCenter , Access access)> GetValidatedEntries(AddAccessBaseDto dto) 
        {
            var searchedDCs = await GetValidatedDataCenters(dto);

            var TargetDbEngines = GetValidatedDbEngines(searchedDCs.sourceDC,searchedDCs.destinationDC, dto);
            
            var validatedAccess = GetValidatedAccess(TargetDbEngines.source,TargetDbEngines.destination,dto);

            return(searchedDCs.sourceDC, validatedAccess);
        }

        private async Task<(DataCenter sourceDC, DataCenter destinationDC)> GetValidatedDataCenters(AddAccessBaseDto dto)
        {
            var searchDto = new SearchDCDto.Create()
                    .AddSourceDcName(dto.FromDCName)
                    .AddDestinationDcName(dto.ToDCName)
                    .AddAccessSourceName(dto.FromName)
                    .AddAccessDestinationName(dto.ToName)
                    .AddAccessSourceAddress(dto.FromAddress)
                    .AddAccessDestinationAddress(dto.ToAddress)
                    .Build();

            var searchedDcs = await _dataCenterSqlRepo.SearchSourceAndDestinationDataCenters(searchDto);

            if (searchedDcs.source == DataCenter.Empty)
                throw new Exception($"No DataCenter with name: {dto.FromDCName} found");

            if (searchedDcs.destination == DataCenter.Empty)
                throw new Exception($"No DataCenter with name: {dto.ToDCName} found");

            return searchedDcs;
        }

        private (DatabaseEngine source, DatabaseEngine destination)
            GetValidatedDbEngines(DataCenter sourceDC, DataCenter destinationDC ,AddAccessBaseDto dto)
        {
            DatabaseEngine source = sourceDC
                .DatabaseEngines
                .FirstOrDefault(x => x.Name == dto.FromName || x.Address == dto.FromAddress) ?? DatabaseEngine.Empty;

            DatabaseEngine destination = destinationDC
                .DatabaseEngines
                .FirstOrDefault(x => x.Name == dto.ToName || x.Address == dto.ToAddress) ?? DatabaseEngine.Empty;

            if (source == DatabaseEngine.Empty)
                throw new Exception($"No source DbEngine with name: {dto.FromName} Or address : {dto.FromAddress} found");
            if (destination == DatabaseEngine.Empty)
                throw new Exception($"No destination DbEngine with name: {dto.ToName} Or address : {dto.ToAddress} found");

            return (source, destination);
        }

        private Access GetValidatedAccess(DatabaseEngine source, DatabaseEngine destination, AddAccessBaseDto dto)
        {
            string serializedSource = JsonConvert.SerializeObject(source);
            string serializedDestination = JsonConvert.SerializeObject(destination);

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
