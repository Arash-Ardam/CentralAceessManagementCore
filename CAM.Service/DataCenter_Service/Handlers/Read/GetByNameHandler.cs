using CAM.Service.DataCenter_Service.Queries;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
using MediatR;

namespace CAM.Service.DataCenter_Service.Handlers.Read
{
    public class GetByNameHandler : IRequestHandler<GetByNameQuery, DataCenter>
    {
        public GetByNameHandler(IReadDataCenterRepository dataCenterRepo)
        {
            DataCenterRepo = dataCenterRepo;
        }
        private IReadDataCenterRepository DataCenterRepo { get; }

        public async Task<DataCenter> Handle(GetByNameQuery request, CancellationToken cancellationToken)
        {
            return await DataCenterRepo.GetDataCenter(request.name) ?? DataCenter.Empty;
        }
    }
}
