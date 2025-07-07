using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataCenterRepo.SyncRepo
{
    public interface IDataCenterSyncRepository
    {
        Task SyncAsync(string dataCenterName);
    }
}
