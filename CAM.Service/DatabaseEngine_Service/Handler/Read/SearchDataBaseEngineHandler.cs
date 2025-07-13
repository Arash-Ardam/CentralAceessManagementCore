using CAM.Service.DatabaseEngine_Service.Queries;
using CAM.Service.Dto;
using CAM.Service.Repository.DataBaseEngineRepo.ReadRepo;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service.Handler.Read
{
    public class SearchDataBaseEngineHandler : IRequestHandler<SearchDataBaseEngineQuery, List<DatabaseEngine>>
    {
        private readonly IReadDataBaseEngineRepo _readRepo;

        public SearchDataBaseEngineHandler(IReadDataBaseEngineRepo readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<List<DatabaseEngine>> Handle(SearchDataBaseEngineQuery request, CancellationToken cancellationToken)
        {
            SearchDCDto searchDCDto = new SearchDCDto.Create()
                .AddSourceDcName(request.Dto.dcName)
                .AddDbEngineName(request.Dto.dbEngineName)
                .AddDbEngineAddress(request.Dto.Address)
                .Build();

            return _readRepo.SearchDataBaseEngine(searchDCDto).Result.ToList();
        }
    }
}
