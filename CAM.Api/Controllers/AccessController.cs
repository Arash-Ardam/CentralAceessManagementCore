using AutoMapper;
using CAM.Api.Dtos;
using CAM.Service.Access_Service;
using CAM.Service.Dto;
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
            var createAccessDto = _mapper.Map<AccessBaseDto>(addAccessDto);
            createAccessDto.ToDCName = createAccessDto.FromDCName;

            var response = await _service.CreateAcceess(createAccessDto);

            return Created();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddOutBoundAccessByName([FromBody] AddOutBoundAccessByNameDto addAccessDto)
        {
            var createAccessDto = _mapper.Map<AccessBaseDto>(addAccessDto);
            var response = await _service.CreateAcceess(createAccessDto);

            return Created();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddInBoundByAddress([FromBody] AddInBoundAccessByAddressDto addAcceessDto)
        {
            var createAccessDto = _mapper.Map<AccessBaseDto>(addAcceessDto);
            createAccessDto.ToDCName = createAccessDto.FromDCName;

            var response = await _service.CreateAcceess(createAccessDto);

            return Created();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddOutBoundByAddress([FromBody] AddOutBoundAccessByAddressDto addAccessDto)
        {
            var createAccessDto = _mapper.Map<AccessBaseDto>(addAccessDto);
            var response = await _service.CreateAcceess(createAccessDto);

            return Created();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Search([FromBody] SearchAccessDto searchDto)
        {
            AccessBaseDto mappedDto = _mapper.Map<AccessBaseDto>(searchDto);

            if (searchDto.DestinationDCName == string.Empty)
                mappedDto.ToDCName = mappedDto.FromDCName;

            List<Access> searchedAccesses = await _service.SearchAccess(mappedDto);

            var result = searchedAccesses.Select(x => _mapper.Map<SearchAccessResultDto>(x)).ToList();

            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Details(short id)
        {
            Access response = await _service.GetAccess(id);

            return Ok(_mapper.Map<SearchAccessResultDto>(response));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            Access entry = await _service.GetAccess(id) ?? Access.Empty;
            if (entry == Access.Empty)
            {
                return NotFound($"Access with id : {id} not found");
            }

            await _service.RemoveAccess(entry);
            return Accepted(entry);

        }

    }
}
