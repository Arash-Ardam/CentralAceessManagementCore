﻿using CAM.Service.Access_Service.Queries;
using CAM.Service.Dto;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.AccessRepo.ReadRepo
{
    public interface IReadAccessRepository
    {
        Task<List<Access>> SearchAccesses(SearchAccessQuery dto);

        Task CreateAccess(AccessBaseDto accessDto);

        Task DeleteAccess(string source, string destination);
    }
}
