﻿using CAM.Service.Dto;
using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.Repository.DataBaseEngineRepo.WriteRepo
{
    public interface IDataBaseEngineRepo
    {
        Task AddDataBaseEngine(string dcName, string dbEngineName, string address);
        Task<DatabaseEngine> GetDataBaseEngine(string name);
        Task RemoveDataBaseEngine(string dcName, string dbEngineName);
    }
}
