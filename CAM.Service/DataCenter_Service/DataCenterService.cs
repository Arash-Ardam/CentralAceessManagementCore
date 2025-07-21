using CAM.Service.Abstractions;
using CAM.Service.DataCenter_Service.Commands;
using CAM.Service.DataCenter_Service.Handlers;
using CAM.Service.DataCenter_Service.Queries;
using CAM.Service.Dto;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataCenter_Service
{
    internal class DataCenterService : IDataCenterService
    {
        private readonly IMediator _mediator;

        public DataCenterService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateDataCenterByName(string name)
        {
            await _mediator.Send(new AddDataCenterByNameCommand(name));
        }

        public async Task DeleteDataCenter(string name)
        {
            await _mediator.Send(new DeleteDataCenterByNameCommand(name));
        }

        public async Task<List<DataCenter>> GetAllDataCenters()
        {
            return await _mediator.Send(new GetAllDataCentersQuery());
        }

        public async Task<DataCenter> GetDataCenter(string name)
        {
            return await _mediator.Send(new GetDataCenterByNameQuery(name)); 
        }
        public async Task<DataCenter> GetDataCenterWithDatabaseEngines(string name)
        {
            return await _mediator.Send(new GetDataCenterWithDatabaseEnginesQuery(name));
        }




    }
}
