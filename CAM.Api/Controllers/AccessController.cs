using CAM.Api.Dtos;
using Domain.DataModels;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CAM.Api.Controllers
{
    public class AccessController : ApiControllerBase
    {
        public AccessController(ILogger<ApiControllerBase> logger) : base(logger)
        {
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> AddInBound([FromBody] AddAccessDto addAccessDto)
        //{
        //    Access access = new Access.Create()
        //        .AddSource(addAccessDto.FromName)
        //        .AddDestination(addAccessDto.ToName)
        //        .SetDirection(DatabaseDirection.InBound)
        //        .Build();

        //    var response = 

        //}

    }
}
