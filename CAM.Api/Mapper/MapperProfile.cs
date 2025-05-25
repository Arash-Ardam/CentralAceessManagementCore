using AutoMapper;
using CAM.Api.Dtos;
using CAM.Service.Dto;

namespace CAM.Api.Mapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<AddInBoundAccessByNameDto, AddAccessBaseDto>()
                .ForMember(x => x.FromDCName, t => t.MapFrom(x => x.DCName));

            CreateMap<AddOutBoundAccessByNameDto, AddAccessBaseDto>();

            CreateMap<AddInBoundAccessByAddressDto, AddAccessBaseDto>()
                .ForMember(x => x.FromDCName, t => t.MapFrom(x => x.DCName));

            CreateMap<AddOutBoundAccessByAddressDto, AddAccessBaseDto>();  

        }
    }
}
