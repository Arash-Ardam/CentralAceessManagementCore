using CAM.Service.Access_Service.Queries;
using CAM.Service.Dto;
using Domain.DataModels;
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

        public async Task CreateAccess(AccessBaseDto accessDto,DataCenter targetDC, Access validAccess)
        {
            await _readDbContext.SaveData(
                storedProcedure: "spAccess_Add",
                parameters: new
                {
                    dcName = targetDC.Name,

                    source = validAccess.Source,
                    sourceName = accessDto.FromName,
                    sourceAddress = accessDto.FromAddress,

                    destination = validAccess.Destination,
                    destinationName = accessDto.ToName,
                    destinationAddress = accessDto.ToAddress,

                    port = validAccess.Port,
                    direction = validAccess.Direction
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
