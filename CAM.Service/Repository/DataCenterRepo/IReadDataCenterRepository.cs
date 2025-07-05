using Domain.DataModels;

namespace CAM.Service.Repository.DataCenterRepo
{
    public interface IReadDataCenterRepository
    {
        Task<DataCenter?> GetDataCenter(string name);

    }
}