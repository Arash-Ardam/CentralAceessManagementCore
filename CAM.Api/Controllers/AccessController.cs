using AutoMapper;
using CAM.Api.Dtos;
using CAM.Service.Access_Service;
using CAM.Service.Dtos;
using Domain.DataModels;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Api.Controllers
{
    public class AccessController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccessService _service;

        public AccessController(ILogger<ApiControllerBase> logger , IMapper mapper , IAccessService accessService) : base(logger)
        {
            _mapper = mapper;
            _service = accessService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddInBoundByName([FromBody] AddInBoundAccessByNameDto addAccessDto)
        {
            var createAccessDto = _mapper.Map<AddAccessByNameDto>(addAccessDto);
            createAccessDto.ToDCName = createAccessDto.FromDCName;

            var response = await _service.CreateAcceess(createAccessDto);

            return Created();

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddOutBoundAccessByName([FromBody] AddOutBoundAccessByNameDto addAccessDto)
        {
            var createAccessDto = _mapper.Map<AddAccessByNameDto>(addAccessDto);
            var response = await _service.CreateAcceess(createAccessDto);

            return Created();
        }

    }
}
