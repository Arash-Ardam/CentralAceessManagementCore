using CAM.Service.Dto;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service
{
    public interface IDatabaseEngineService
    {
        Task AddDatabaseEngine(string dcName, string dbEngineName, string address);

        Task<List<DatabaseEngine>> Search(SearchDbEngineDto searchDto);
        
        Task Remove(string dcName, string dbEngineName);

    }
}
