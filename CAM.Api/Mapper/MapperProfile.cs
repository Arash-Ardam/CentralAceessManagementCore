using AutoMapper;
using CAM.Api.Dtos;
using CAM.Service.Dto;
using Domain.DataModels;
using Newtonsoft.Json;

namespace CAM.Api.Mapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<AddInBoundAccessByNameDto, AccessBaseDto>()
                .ForMember(x => x.FromDCName, t => t.MapFrom(x => x.DCName));

            CreateMap<AddOutBoundAccessByNameDto, AccessBaseDto>();

            CreateMap<AddInBoundAccessByAddressDto, AccessBaseDto>()
                .ForMember(x => x.FromDCName, t => t.MapFrom(x => x.DCName));

            CreateMap<AddOutBoundAccessByAddressDto, AccessBaseDto>();

            CreateMap<Access, SearchAccessResultDto>()
                .ForMember(src => src.Source,opt => opt.MapFrom<JsonSourceToDataBaseEngineResolver>())
                .ForMember(dst => dst.Destination, opt => opt.MapFrom<JsonDestinationToDataBaseEngineResolver>());

            CreateMap<SearchAccessDto, AccessBaseDto>()
                .ForMember(x => x.FromDCName, t => t.MapFrom(x => x.DCName))
                .ForMember(x=>x.ToDCName,t => t.MapFrom(x => x.DCName));


        }
    }

    public class JsonSourceToDataBaseEngineResolver : IValueResolver<Access, SearchAccessResultDto, DatabaseEngine>
    {
        public DatabaseEngine Resolve(Access source, SearchAccessResultDto destination, DatabaseEngine destMember, ResolutionContext context)
        {
            return string.IsNullOrWhiteSpace(source.Source)
                ? DatabaseEngine.Empty
                : JsonConvert.DeserializeObject<DatabaseEngine>(source.Source);
        }
    }

    public class JsonDestinationToDataBaseEngineResolver : IValueResolver<Access, SearchAccessResultDto, DatabaseEngine>
    {
        public DatabaseEngine Resolve(Access source, SearchAccessResultDto destination, DatabaseEngine destMember, ResolutionContext context)
        {
            return string.IsNullOrWhiteSpace(source.Destination)
                ? DatabaseEngine.Empty
                : JsonConvert.DeserializeObject<DatabaseEngine>(source.Destination);
        }
    }

}
