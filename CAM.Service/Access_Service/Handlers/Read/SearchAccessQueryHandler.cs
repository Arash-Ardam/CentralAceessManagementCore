using CAM.Service.Access_Service.Queries;
using CAM.Service.Repository.AccessRepo.ReadRepo;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service.Handlers.Read
{
    public class SearchAccessQueryHandler : IRequestHandler<SearchAccessQuery, List<Access>>
    {
        private readonly IReadAccessRepository _readRepo;

        public SearchAccessQueryHandler(IReadAccessRepository readRepo)
        {
            _readRepo = readRepo;
        }

        public async Task<List<Access>> Handle(SearchAccessQuery request, CancellationToken cancellationToken)
        {
            return await _readRepo.SearchAccesses(request.dto);
        }
    }
}
