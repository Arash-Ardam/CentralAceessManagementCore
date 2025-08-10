using CAM.Api.Dtos;
using CAM.Service;
using CAM.Service.DataCenter_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Api.Controllers.Admin
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/admin/[controller]")]
    public class DataCenterController : ApiControllerBase
    {
        private readonly IDataCenterService _dcService;

        public DataCenterController(ILogger<ApiControllerBase> logger, IDataCenterService servise) : base(logger)
        {
            _dcService = servise;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                await _dcService.CreateDataCenterByName(name);
                return Created();
            }
            else
            {
                return BadRequest(string.Format(Messages.StringNullOrWhiteSpace, "نام دیتابیس"));
            }
        }



        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _dcService.GetDataCenter(name);
            return Ok(response);
        }


        [HttpGet("[action]/{dcName}")]
        public async Task<IActionResult> GetWithDbEngines(string dcName)
        {
            var response = await _dcService.GetDataCenterWithDatabaseEngines(dcName);
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dcService.GetAllDataCenters());
        }

        [HttpDelete("[action]/{name}")]
        public async Task<IActionResult> Remove(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                await _dcService.DeleteDataCenter(name);
                return Accepted();
            }
            else
            {
                return BadRequest(string.Format(Messages.StringNullOrWhiteSpace, "نام"));
            }
        }

    }
}
