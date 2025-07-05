using CAM.Service.Abstractions;
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
        private readonly IRepoUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public DataCenterService(IRepoUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task CreateDataCenterByName(string name)
        {
            await _unitOfWork.DataCenterRepo.AddDataCenter(DataCenter.CreateByName(name));
        }

        public async Task DeleteDataCenter(string name)
        {
            await _unitOfWork.DataCenterRepo.DeleteDataCenter(name);
        }

        public async Task EditDataCenterName(string oldName, string newName)
        {
            await _unitOfWork.DataCenterRepo.UpdateDataCenter(oldName, newName);
        }

        public async Task<List<DataCenter>> GetAllDataCenters()
        {
            return await _unitOfWork.DataCenterRepo.GetAllDataCenters();
        }

        public async Task<DataCenter> GetDataCenter(string name)
        {
            return await _mediator.Send(new GetByNameQuery(name)); 
        }
        public async Task<DataCenter> GetDataCenterWithDatabaseEngines(string name)
        {
            SearchDCDto searchDCDto = new SearchDCDto.Create()
                .AddSourceDcName(name)
                .Build();

            return await _unitOfWork.DataCenterRepo.SearchDataCenter<BasePredicateBuilder>(searchDCDto);
        }
    }
}
