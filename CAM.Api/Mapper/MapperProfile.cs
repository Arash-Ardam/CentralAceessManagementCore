using AutoMapper;
using CAM.Api.Dtos;
using CAM.Service.Dtos;

namespace CAM.Api.Mapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<AddInBoundAccessByNameDto, AddAccessByNameDto>();
        }
    }
}
