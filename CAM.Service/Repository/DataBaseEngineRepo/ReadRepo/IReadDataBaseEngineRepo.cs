using CAM.Service.Dto;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataBaseEngineRepo.ReadRepo
{
    public interface IReadDataBaseEngineRepo
    {
        Task AddDataBaseEngine(string name,string address,string dcName);
        Task DeleteDataBaseEngine(string name,string dcName);

        Task<DatabaseEngine> GetDatabaseEngine(string dcName,string name);
        Task<IEnumerable<DatabaseEngine>> SearchDataBaseEngine(SearchDCDto searchDCDto);
    }
}
