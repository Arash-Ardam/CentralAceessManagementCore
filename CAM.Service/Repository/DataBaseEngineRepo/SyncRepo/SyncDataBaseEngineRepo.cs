using CAM.Service.Repository.DataBaseEngineRepo.ReadRepo;
using CAM.Service.Repository.DataBaseEngineRepo.WriteRepo;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataBaseEngineRepo.SyncRepo
{
    internal class SyncDataBaseEngineRepo : ISyncDatabaseEngineRepo
    {
        private readonly IReadDataBaseEngineRepo _readRepo;
        private readonly IDataBaseEngineRepo _writeRepo;

        public SyncDataBaseEngineRepo(IReadDataBaseEngineRepo readRepo, IDataBaseEngineRepo writeRepo)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
        }

        public async Task SyncDatabaseEngine(string dcName, string name, string address)
        {
            var existedDataBaseEngine = _writeRepo.SearchDataBaseEngine(new Dto.SearchDbEngineDto()
            {
                dcName = dcName,
                dbEngineName = name
            })
                .Result
                .FirstOrDefault() ?? DatabaseEngine.Empty;

            if (existedDataBaseEngine != DatabaseEngine.Empty)
                await _readRepo.AddDataBaseEngine(name, address,dcName);

            else
                await _readRepo.DeleteDataBaseEngine(name, dcName);
        }
    }
}
