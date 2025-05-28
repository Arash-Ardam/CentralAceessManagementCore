using CAM.Service.Abstractions;
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
        Task<DataCenter> SearchDataCenter<TPredicator>(SearchDCDto dto) where TPredicator : IPredicateBuilder, new();
        Task<List<DataCenter>> GetAllDataCenters();

    }
}
