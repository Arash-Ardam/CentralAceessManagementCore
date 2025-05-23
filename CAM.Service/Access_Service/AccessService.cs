using CAM.Service.Dto;
using CAM.Service.Dtos;
using CAM.Service.Repository.AccessRepo;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
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
        public async Task<Access> CreateAccess(AddAccessByNameDto dto)
        {
            var validatedEntries = await GetValidatedEntries(dto);
            bool isAccessExist = _accessRepo.AnyAccessExist(validatedEntries.dataCenter, validatedEntries.access, dto.Port);

            if (isAccessExist)
                throw new Exception("Alredy Exist");

           return  await _accessRepo.CreateAccess(validatedEntries.dataCenter, validatedEntries.access);
        }



        private async Task<(DataCenter dataCenter , Access access)> GetValidatedEntries(AddAccessByNameDto dto) 
        {
            SearchDCDto searchDCDto = new SearchDCDto.Create()
                .AddDcName(dto.DCName)
                .AddAccessSourceName(dto.FromName)
                .AddAccessDestinationName(dto.ToName)
                .Build();

            var existedDataCenter = await _dataCenterSqlRepo.GetDataCenterWithParams(searchDCDto);

            if (existedDataCenter == DataCenter.Empty)
                throw new Exception($"No DataCenter with name: {dto.DCName} found");

            if (!existedDataCenter.DatabaseEngines.Any(x => x.Name == dto.FromName))
                throw new Exception($"No Source DbEngine with name: {dto.FromName} found");

            if (!existedDataCenter.DatabaseEngines.Any(x => x.Name == dto.ToName))
                throw new Exception($"No Destination DbEngine with name: {dto.ToName} found");

            DatabaseEngine source = existedDataCenter.DatabaseEngines[0];
            DatabaseEngine destination = existedDataCenter.DatabaseEngines[1];

            var serializedDbEngines = ConvertSourceAndDestination(source, destination);

            Access validatedAccess = new Access.Create()
                .AddSource(serializedDbEngines.jsonSource)
                .AddPort(dto.Port)
                .AddDestination(serializedDbEngines.jsonDestination)
                .SetDirection(dto.Direction)
                .Build();

            return(existedDataCenter, validatedAccess);
        }

        private (string jsonSource,string jsonDestination) ConvertSourceAndDestination(DatabaseEngine source, DatabaseEngine destination)
        {
            string serializedSource = JsonConvert.SerializeObject(source);
            string serializedDestination = JsonConvert.SerializeObject(destination);

            return(serializedSource,serializedDestination);
        }

    }
}
