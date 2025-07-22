using CAM.Service.Abstractions;
using CAM.Service.DatabaseEngine_Service.Commands;
using CAM.Service.DatabaseEngine_Service.Queries;
using CAM.Service.DataCenter_Service.Queries;
using CAM.Service.Dto;
using CAM.Service.Repository.DataBaseEngineRepo;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service
{
    internal class DatabaseEngineService : IDatabaseEngineService
    {
        private readonly IMediator _mediator;

        public DatabaseEngineService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task AddDatabaseEngine(string dcName, string dbEngineName, string address)
        {
            var result = await CheckUniqueDbEngine(dcName, dbEngineName, address);
            if (!result.isSuccess)
                throw new Exception(result.Message);

            await _mediator.Send(new AddDataBaseEngineCommand(dcName, dbEngineName, address));
        }



        public async Task Remove(string dcName, string engineName)
        {
            var result = await CheckUniqueDbEngine(dcName, engineName, string.Empty);
            if (result.isSuccess)
                throw new Exception(result.Message);

            await _mediator.Send(new DeleteDataBaseEngineCommand(dcName, engineName));
        }

        public Task<List<DatabaseEngine>> Search(SearchDbEngineDto searchDto)
        {
            return _mediator.Send(new SearchDataBaseEngineQuery(searchDto));
        }


        private async Task<ValidationResponse> CheckUniqueDbEngine(string dcName, string dbEngineName, string address)
        {
            ValidationResponse response = new ValidationResponse();
            var searchDto = new SearchDbEngineDto()
            {
                dcName = dcName,
                dbEngineName = dbEngineName,
                Address = address
            };

            var existedDataCenter = await _mediator.Send(new GetDataCenterByNameQuery(dcName));

            var existedDbEngines = await _mediator.Send(new SearchDataBaseEngineQuery(searchDto));


            if(existedDataCenter == DataCenter.Empty)
            {
                response.isSuccess = false;
                response.Message = @"Target DataCenter is not found";
            }

            else if (existedDbEngines.Count != 0)
            {
                bool isDbEngineNameDuplicated = existedDbEngines[0].Name == dbEngineName;
                bool isAddressDuplicated = existedDbEngines[0].Address == address;

                if (isDbEngineNameDuplicated)
                {
                    response.isSuccess = false;
                    response.Message = $"Entity with Param : {dbEngineName} Already Exist";
                }

                if (isAddressDuplicated)
                {
                    response.isSuccess = false;
                    response.Message = $"Entity with Param : {address} Already Exist";
                }
            }
            else
            {
                response.isSuccess = true;
                response.Message = $"Entity with Param : {dbEngineName} , {address} Not Exist";
            }

            return response;
        }

    }
}
