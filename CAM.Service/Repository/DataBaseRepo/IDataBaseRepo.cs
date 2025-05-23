using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataBaseRepo
{
    public interface IDataBaseRepo
    {
        Task AddDataBaseToDataBaseEngine(string dcName, string dbEngineName, string dbName);
        Task<List<Database>> SearchDataBase(string dcName, string dbEngineName, string dbName);
    }
}
