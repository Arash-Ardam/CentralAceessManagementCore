
using AutoMapper;
using CAM.Api.Dtos;
using CAM.Service.Access_Service;
using CAM.Service.DatabaseEngine_Service;
using CAM.Service.DataCenter_Service;
using CAM.Service.Dto;
using Domain.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace CAM.Api.Controllers.Users
{
    [Authorize]
    [Route("api/users/[controller]")]
    public class DataBaseEngineController : ApiControllerBase
    {
        private readonly IDatabaseEngineService _dbEngineService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDataCenterService _dataCenterService;

        public DataBaseEngineController(
            IDatabaseEngineService engineService,
            IHttpContextAccessor httpContextAccessor,
            IDataCenterService dataCenterService,
            ILogger<ApiControllerBase> logger) : base(logger)
        {
            _dbEngineService = engineService;
            _contextAccessor = httpContextAccessor;
            _dataCenterService = dataCenterService;

            TenantId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "tenant-id").Value;
        }

        private string TenantId { get; set; }



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
                var existedTenant = await _dataCenterService.GetDataCenter(TenantId);
                if (existedTenant == DataCenter.Empty)
                    throw new Exception("Tenant not found");

                await _dbEngineService.AddDatabaseEngine(TenantId, dto.dbEngineName, dto.Address);
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
