using CAM.Service.Abstractions;
using CAM.Service.Dto;
using CAM.Service.Repository.AccessRepo;
using CAM.Service.Repository.DataCenterRepo;
using Domain.DataModels;
using Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAM.Service.Access_Service
{
    internal class AccessService : IAccessService
    {
        private readonly AccessValidator _validator;
        private readonly IAccessRepository _accessRepo;

        public AccessService(AccessValidator validator, IAccessRepository accessRepo)
        {
            _validator = validator;
            _accessRepo = accessRepo;
        }
        public async Task<Access> CreateAcceess(AccessBaseDto dto)
        {
            var validatedEntries = await _validator.GetValidatedEntries(dto);
            bool isAccessExist = _accessRepo.AnyAccessExist(validatedEntries.Source, validatedEntries.ValidatedAccess, dto.Port);

            if (isAccessExist)
                throw new Exception("Alredy Exist");

           return  await _accessRepo.CreateAccess(validatedEntries.Source, validatedEntries.ValidatedAccess);
        }

        public async Task<List<Access>> SearchAccess(AccessBaseDto dto)
        {
            var searchAccessDto =  await _validator.GetValidatedSearchEntry(dto);

            return _accessRepo.SearchAccess(searchAccessDto);
        }


        

    }
}
