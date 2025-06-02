using CAM.Service.Abstractions;
using CAM.Service.Dto;
using CAM.Service.Repository.DataBaseRepo;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataBase_Service
{
    internal class DataBaseService : IDataBaseService
    {
        private readonly IRepoUnitOfWork _unitOfWork;

        public DataBaseService(IRepoUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddDataBaseToDataBaseEngine(string dcName, string engineName, string dbName)
        {
            await _unitOfWork.DataBaseRepo.AddDataBaseToDataBaseEngine(dcName, engineName, dbName);
        }

        public async Task<List<Database>> SearchDataBase(SearchDataBaseSto searchDto)
        {
            return await _unitOfWork.DataBaseRepo.SearchDataBase(searchDto.DataCenterName, 
                searchDto.DataBaseEngineName, searchDto.DataBaseName);
        }
    }
}
