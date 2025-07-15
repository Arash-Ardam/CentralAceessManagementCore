using CAM.Service.Access_Service.Queries;
using CAM.Service.Dto;
using Domain.DataModels;
using Newtonsoft.Json;
using ReadDbContext;
using System;
using System.Collections.Generic;
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
            await _readDbContext.SaveData(
                storedProcedure: "spAccess_Add",
                parameters: new
                {
                    dcName = accessDto.FromDCName,

                    source = JsonConvert.SerializeObject(DatabaseEngine.CreateByNameAndAddress(accessDto.FromName,accessDto.FromAddress)),
                    sourceName = accessDto.FromName,
                    sourceAddress = accessDto.FromAddress,

                    destination = JsonConvert.SerializeObject(DatabaseEngine.CreateByNameAndAddress(accessDto.ToName, accessDto.ToAddress)),
                    destinationName = accessDto.ToName,
                    destinationAddress = accessDto.ToAddress,

                    port = accessDto.Port,
                    direction = accessDto.Direction
                });
                
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

        public async Task<List<Access>> SearchAccesses(SearchAccessQuery dto)
        {
            var result = await _readDbContext.LoadData<Access, dynamic>(
                storedProcedure: "spAccess_Search",
                parameters: new 
                {
                    dcName = dto.dto.dcName,

                    sourceName = dto.dto.sourceName,
                    sourceAddress = dto.dto.sourceAddress,

                    destinationName = dto.dto.destinationName,
                    destinationAddress = dto.dto.destinationAddress,

                    port = dto.dto.port
                });

            return result.ToList();
        }
    }
}
