using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataBaseEngineRepo.SyncRepo
{
    public interface ISyncDatabaseEngineRepo
    {
        Task SyncDatabaseEngine(string dcName,string name,string address);
    }
}
