
using Domain.DataModels;

namespace CAM.Service.DataCenter_Service
{
    public interface IDataCenterService
    {
        Task CreateDataCenterByName(string name);

        Task<DataCenter> GetDataCenter(string name);
        Task<List<DataCenter>> GetAllDataCenters();
        Task<DataCenter> GetDataCenterWithDatabaseEngines(string name);

        Task DeleteDataCenter(string name);
    }
}
