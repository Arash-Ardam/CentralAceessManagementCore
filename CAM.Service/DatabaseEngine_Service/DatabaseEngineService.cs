using CAM.Service.Abstractions;
using CAM.Service.Dto;
using CAM.Service.Repository.DataBaseEngineRepo;
using Domain.DataModels;
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
        private readonly IRepoUnitOfWork _unitOfWork;

        public DatabaseEngineService(IRepoUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task AddDatabaseEngine(string dcName, string dbEngineName, string address)
        {
           await _unitOfWork.DataBaseEngineRepo.AddDataBaseEngine(dcName, dbEngineName, address);
        }

        public async Task Remove(string dcName, string engineName)
        {
            await _unitOfWork.DataBaseEngineRepo.RemoveDataBaseEngine(dcName, engineName);
        }

        public Task<List<DatabaseEngine>> Search(SearchDbEngineDto searchDto)
        {
            return _unitOfWork.DataBaseEngineRepo.SearchDataBaseEngine(searchDto);
        }
    }
}
