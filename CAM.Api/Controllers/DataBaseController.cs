using CAM.Api.Dtos;
using CAM.Service.DataBase_Service;
using CAM.Service.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Api.Controllers
{
    public class DataBaseController : ApiControllerBase
    {
        private readonly IDataBaseService _service;

        public DataBaseController(IDataBaseService dataBaseService,ILogger<ApiControllerBase> logger) : base(logger)
        {
            _service = dataBaseService;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Add(AddDataBaseDto dto)
        {
            Tuple<bool,string> hasNull = CheckNulity(dto);

            if (hasNull.Item1)
                return BadRequest(string.Format(Messages.StringNullOrWhiteSpace, hasNull.Item2));

            else
            {
                await _service.AddDataBaseToDataBaseEngine(dto.DataCenterName, dto.DataBaseEngineName, dto.DataBaseName);
                return Created();
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Search(SearchDataBaseSto searchDto)
        {
            var result = await _service.SearchDataBase(searchDto);
            return Ok(result);
        }
    }
}
