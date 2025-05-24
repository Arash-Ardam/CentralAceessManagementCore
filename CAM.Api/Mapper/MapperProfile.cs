using AutoMapper;
using CAM.Api.Dtos;
using CAM.Service.Dtos;

namespace CAM.Api.Mapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<AddInBoundAccessByNameDto, AddAccessByNameDto>()
                .ForMember(x => x.FromDCName, t => t.MapFrom(x => x.DCName));

            CreateMap<AddOutBoundAccessByNameDto, AddAccessByNameDto>();

        }
    }
}
