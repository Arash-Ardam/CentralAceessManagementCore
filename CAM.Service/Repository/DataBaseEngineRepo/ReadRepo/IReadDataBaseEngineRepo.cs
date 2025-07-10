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
    }
}
