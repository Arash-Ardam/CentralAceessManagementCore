﻿using CAM.Service.Abstractions;
using CAM.Service.DatabaseEngine_Service.Commands;
using CAM.Service.DatabaseEngine_Service.Queries;
using CAM.Service.Dto;
using CAM.Service.Repository.DataBaseEngineRepo;
using Domain.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Service.DatabaseEngine_Service
{
    internal class DatabaseEngineService : IDatabaseEngineService
    {
        private readonly IMediator _mediator;

        public DatabaseEngineService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task AddDatabaseEngine(string dcName, string dbEngineName, string address)
        {
            await _mediator.Send(new AddDataBaseEngineCommand(dcName, dbEngineName, address));
        }

        public async Task Remove(string dcName, string engineName)
        {
            await _mediator.Send(new DeleteDataBaseEngineCommand(dcName, engineName));
        }

        public Task<List<DatabaseEngine>> Search(SearchDbEngineDto searchDto)
        {
            return _mediator.Send(new SearchDataBaseEngineQuery(searchDto));
        }
    }
}
