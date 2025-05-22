using CAM.Service.Dto;
using Domain.DataModels;

namespace CAM.Service.Repository
{
    public interface IDataCenterSqlDataRepository
    {
        Task AddDataCenter(DataCenter dataCenter);
        Task DeleteDataCenter(string name);
        Task UpdateDataCenter(string oldName, string newName);
        Task<DataCenter> GetDataCenter(string name);
        Task<DataCenter> GetDataCenterWithEngines(string name);
        Task<DataCenter> TryGetDataCenterWithGivenParams(string dcName,string dbEngineName,string address);
        Task<List<DataCenter>> GetAllDataCenters();
        
        Task AddDataBaseEngine(string dcName, string dbEngineName, string address);
        Task<DatabaseEngine> GetDataBaseEngine(string name);
        Task<List<DatabaseEngine>> SearchDataBaseEngine(SearchDbEngineDto searchDto);
        Task RemoveDataBaseEngine(string dcName, string dbEngineName);

        Task AddDataBaseToDataBaseEngine(string dcName, string dbEngineName, string dbName);
        Task<List<Database>> SearchDataBase(string dcName, string dbEngineName, string dbName);
        Task SaveChangesAsync();
    }
}
