using CAM.Service.Dto;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DataBase_Service
{
    public interface IDataBaseService
    {
        Task AddDataBaseToDataBaseEngine(string dcName, string engineName, string dbName);

        Task<List<Database>> SearchDataBase(SearchDataBaseSto searchDto);
    }
}
