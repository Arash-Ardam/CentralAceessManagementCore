using Domain.DataModels;

namespace CAM.Service.Repository.DataCenterRepo.ReadRepo
{
    public interface IReadDataCenterRepository
    {
        Task<List<DataCenter>> GetAllDataCenters();

        Task<DataCenter?> GetDataCenter(string name);

        Task<DataCenter> GetDataCenterWithDatabaseEngines(string name);

        Task AddDataCenter(string name);

        Task DeleteDataCenter(string name);


    }
}