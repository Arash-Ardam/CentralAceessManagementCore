using Domain.DataModels;
using ReadSqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataCenterRepo
{
    internal class ReadDataCenterRepository : IReadDataCenterRepository
    {
        private readonly IReadDataAccess _readDataAccess;

        public ReadDataCenterRepository(IReadDataAccess readDataAccess)
        {
            _readDataAccess = readDataAccess;
        }


        public async Task<DataCenter?> GetDataCenter(string name)
        {
            var dataCenters = await _readDataAccess.LoadData<DataCenter, dynamic>("spDataCenter_Get", new { name = name});
            return dataCenters.FirstOrDefault();
        }

    }
}
