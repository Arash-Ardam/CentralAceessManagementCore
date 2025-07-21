using CAM.Service.Access_Service.Queries;
using CAM.Service.Dto;
using Dapper;
using Domain.DataModels;
using Newtonsoft.Json;
using ReadDbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.AccessRepo.ReadRepo
{
    internal class ReadAccessRepository : IReadAccessRepository
    {
        private readonly IReadDataAccess _readDbContext;

        public ReadAccessRepository(IReadDataAccess readDataAccess)
        {
            _readDbContext = readDataAccess;
        }

        public async Task CreateAccess(AccessBaseDto accessDto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@dcName", accessDto.FromDCName);

            parameters.Add("@source",
                           JsonConvert.SerializeObject(DatabaseEngine.CreateByNameAndAddress(accessDto.FromName, accessDto.FromAddress)),
                           DbType.String,
                           size: -1); 
            parameters.Add("@sourceName", accessDto.FromName);
            parameters.Add("@sourceAddress", accessDto.FromAddress);

            parameters.Add("@destination",
                           JsonConvert.SerializeObject(DatabaseEngine.CreateByNameAndAddress(accessDto.ToName, accessDto.ToAddress)),
                           DbType.String,
                           size: -1);
            parameters.Add("@destinationName", accessDto.ToName);
            parameters.Add("@destinationAddress", accessDto.ToAddress);

            parameters.Add("@sourceDCName", accessDto.FromDCName);
            parameters.Add("@destinationDCName", accessDto.ToDCName);

            parameters.Add("@port", accessDto.Port);
            parameters.Add("@direction", accessDto.Direction);


            await _readDbContext.SaveData(
                storedProcedure: "spAccess_Add",
                parameters: parameters);
        }


        public async Task DeleteAccess(string source, string destination)
        {
            await _readDbContext.SaveData(
                           storedProcedure: "spAccess_Delete",
                           parameters: new
                           {
                               source = source,
                               destination = destination
                           });
        }

        public async Task<List<Access>> SearchAccesses(AccessBaseDto dto)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@sourceDCName", dto.FromDCName);
            parameters.Add("@sourceName", dto.FromName);
            parameters.Add("@sourceAddress", dto.FromAddress);

            parameters.Add("@destinationDCName", dto.ToDCName);
            parameters.Add("@destinationName", dto.ToName);
            parameters.Add("@destinationAddress", dto.ToAddress);


            parameters.Add("@port", dto.Port);
            parameters.Add("@direction", dto.Direction);

            var result = await _readDbContext.LoadData<Access, dynamic>(
                storedProcedure: "spAccess_Search",
                parameters: parameters);

            return result.ToList();
        }
    }
}
