
using AutoMapper;
using CAM.Api.Dtos;
using CAM.Service.Access_Service;
using CAM.Service.DatabaseEngine_Service;
using CAM.Service.DataCenter_Service;
using CAM.Service.Dto;
using Domain.DataModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace CAM.Api.Controllers.Users
{
    [Route("api/users/[controller]")]
    public class DataBaseEngineController : ApiControllerBase
    {
        private readonly IDatabaseEngineService _dbEngineService;
        private readonly IAccessService _accessService;
        private readonly IMapper _mapper;

        public DataBaseEngineController(
            IDatabaseEngineService engineService,
            IAccessService accessService,
            IMapper mapper,
            ILogger<ApiControllerBase> logger) : base(logger)
        {
            _dbEngineService = engineService;
            _accessService = accessService;
            _mapper = mapper;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddDataBaseEngineDto dto)
        {
            Tuple<bool, string> anyNull = CheckNulity(dto);

            if (anyNull.Item1)
            {
                return BadRequest(string.Format(Messages.StringNullOrWhiteSpace, anyNull.Item2));
            }

            else
            {
                await _dbEngineService.AddDatabaseEngine(dto.dcName, dto.dbEngineName, dto.Address);
                return Created();
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Search(SearchDbEngineDto searchDto)
        {
            var engines = await _dbEngineService.Search(searchDto);

            return Ok(engines);
        }

        [HttpDelete("[action]/{dcName}/{engineName}")]
        public async Task<IActionResult> Remove(string dcName, string engineName)
        {
            if (string.IsNullOrEmpty(dcName))
                return BadRequest(string.Format(Messages.StringNullOrWhiteSpace, "DataBase"));
            if (string.IsNullOrEmpty(engineName))
                return BadRequest(string.Format(Messages.StringNullOrWhiteSpace, "DataBaseEngine"));

            var entry = await _dbEngineService.Search(new SearchDbEngineDto() { dcName = dcName, dbEngineName = engineName });
            if (entry.Count == 0)
                return NotFound($"DbEngine with name: {engineName} not found");


            await _dbEngineService.Remove(dcName, engineName);
            return Accepted();

        }

    }
}
