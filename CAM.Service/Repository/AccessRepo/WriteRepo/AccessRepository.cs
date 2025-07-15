using Azure.Core;
using CAM.Service.Dto;
using Domain.DataModels;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.AccessRepo.WriteRepo
{
    internal class AccessRepository : IAccessRepository
    {
        private readonly ApplicationDbContext.ApplicationDbContext _dbContext;
        public AccessRepository(ApplicationDbContext.ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Access> CreateAccess(AccessBaseDto dto)
        {
            Access entryAccess = new Access.Create()
                .AddSource(DatabaseEngine.CreateByNameAndAddress(dto.FromName, dto.FromAddress))
                .AddDestination(DatabaseEngine.CreateByNameAndAddress(dto.ToName, dto.ToAddress))
                .AddPort(dto.Port)
                .SetDirection(dto.Direction)
                .Build();

            DataCenter existedDataCenter = _dbContext.DataCenters.First(x => x.Name == dto.FromDCName) ?? DataCenter.Empty;
            existedDataCenter.AddAccess(entryAccess);

            _dbContext.Entry(existedDataCenter).Collection(x => x.Accesses).IsModified = true;

            await _dbContext.SaveChangesAsync();
            return entryAccess;
        }

        public Access? GetAccess(short id)
        {
            return _dbContext.Access.FirstOrDefault(ac => ac.Id == id);
        }

        public List<Access> GetRangeAccessByDbEngine(string jsonDbEngine)
        {
            return _dbContext.Access.Where(ac => ac.Source == jsonDbEngine || ac.Destination == jsonDbEngine).ToList();
        }

        public bool AnyAccessExist(DataCenter dataCenter, Access access, int port)
        {
            return _dbContext
                .Entry(dataCenter)
                .Collection(dc => dc.Accesses)
                .Query()
                .Any(ac => ac.Source == access.Source && ac.Destination == access.Destination && ac.Port == port);
        }

        public async Task RemoveAccess(Access entry)
        {
            _dbContext.Access.Remove(entry);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRangeOfAccesses(List<Access> accessList)
        {
            _dbContext.Access.RemoveRange(accessList);

            await _dbContext.SaveChangesAsync();
        }

        public List<Access> SearchAccess(SearchAccessBaseDto searchAccessDto)
        {
            if (searchAccessDto == SearchAccessBaseDto.Empty)
            {
                return _dbContext.Access.ToList();
            }

            return _dbContext.Access.Where(GetSearchPredicate(searchAccessDto)).ToList() ?? new List<Access>();
        }

        private ExpressionStarter<Access> GetSearchPredicate(SearchAccessBaseDto searchAccessDto)
        {
            var predicate = PredicateBuilder.New<Access>(true);

            if (searchAccessDto.HasSource())
                predicate.And(ac => ac.Source == searchAccessDto.Source);

            if (!searchAccessDto.HasSource())
            {
                if (searchAccessDto.HasSourceDCName() && searchAccessDto.SourceDbEs != default)
                {
                    predicate.And(ac => searchAccessDto.SourceDbEs.Contains(ac.Source));
                }
            }

            if (searchAccessDto.HasDestination())
                predicate.And(ac => ac.Destination == searchAccessDto.Destination);

            if (!searchAccessDto.HasDestination())
            {
                if (searchAccessDto.HasDestinationDCName() && searchAccessDto.DestinationDCName != searchAccessDto.SourceDCName)
                {
                    if (searchAccessDto.DestinationDbEs != default)
                        predicate.And(ac => searchAccessDto.DestinationDbEs.Contains(ac.Destination));
                }
            }

            if (searchAccessDto.HasPort())
                predicate.And(ac => ac.Port == searchAccessDto.Port);

            if (searchAccessDto.HasDirection())
                predicate.And(ac => ac.Direction == searchAccessDto.Direction);

            return predicate;
        }


    }
}
