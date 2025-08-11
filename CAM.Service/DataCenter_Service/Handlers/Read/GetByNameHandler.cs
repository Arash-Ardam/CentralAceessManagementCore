using CAM.Service.DataCenter_Service.Queries;
using CAM.Service.Repository.DataCenterRepo.ReadRepo;
using CAM.Service.Repository.DataCenterRepo.WriteRepo;
using Domain.DataModels;
using MediatR;

namespace CAM.Service.DataCenter_Service.Handlers.Read
{
    public class GetByNameHandler : IRequestHandler<GetDataCenterByNameQuery, DataCenter>
    {
        public GetByNameHandler(IDataCenterSqlDataRepository dataCenterRepo)
        {
            DataCenterRepo = dataCenterRepo;
        }
        private IDataCenterSqlDataRepository DataCenterRepo { get; }

        public async Task<DataCenter> Handle(GetDataCenterByNameQuery request, CancellationToken cancellationToken)
        {
            return await DataCenterRepo.GetDataCenter(request.name) ?? DataCenter.Empty;
        }
    }
}
