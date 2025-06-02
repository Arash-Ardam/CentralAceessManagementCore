using CAM.Service.Abstractions;
using CAM.Service.Dto;
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
        private readonly IRepoUnitOfWork _unitOfWork;

        public AccessService(AccessValidator validator, IRepoUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task<Access> CreateAcceess(AccessBaseDto dto)
        {
            var validatedEntries = await _validator.GetValidatedEntries(dto);
            bool isAccessExist = _unitOfWork.AccessRepository.AnyAccessExist(validatedEntries.Source, validatedEntries.ValidatedAccess, dto.Port);

            if (isAccessExist)
                throw new Exception("Alredy Exist");

           return  await _unitOfWork.AccessRepository.CreateAccess(validatedEntries.Source, validatedEntries.ValidatedAccess);
        }

        public async Task<List<Access>> SearchAccess(AccessBaseDto dto)
        {
            var searchAccessDto =  await _validator.GetValidatedSearchEntry(dto);

            return _unitOfWork.AccessRepository.SearchAccess(searchAccessDto);
        }


        

    }
}
