using CAM.Service.Dto;
using Domain.DataModels;

namespace CAM.Service.Repository.DataCenterRepo
{
    public interface IDataCenterSqlDataRepository
    {
        Task AddDataCenter(DataCenter dataCenter);
        Task DeleteDataCenter(string name);
        Task UpdateDataCenter(string oldName, string newName);
        Task<DataCenter> GetDataCenter(string name);
        Task<DataCenter> GetDataCenterWithParams(SearchDCDto dto);
        Task<(DataCenter source, DataCenter destination)> SearchSourceAndDestinationDataCenters(SearchDCDto dto);
        Task<List<DataCenter>> GetAllDataCenters();

    }
}
