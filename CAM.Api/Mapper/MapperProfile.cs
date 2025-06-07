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

            CreateMap<SearchAccessDto, AccessBaseDto>()
                .ForMember(x => x.FromDCName, t => t.MapFrom(x => x.SourceDCName))
                .ForMember(x => x.ToDCName, t => t.MapFrom(x => x.DestinationDCName));

            CreateMap<Access, SearchAccessResultDto>()
                .ForMember(src => src.Source,opt => opt.MapFrom(new DataBaseEngineResolver<SearchAccessResultDto>(x => x.Source)))
                .ForMember(dst => dst.Destination, opt => opt.MapFrom(new DataBaseEngineResolver<SearchAccessResultDto>(x => x.Destination)));

        }
    }


    public class DataBaseEngineResolver<TDestination> : IValueResolver<Access, TDestination, DatabaseEngine>
    {
        private readonly Func<Access, object> _propertyAccessor;

        public DataBaseEngineResolver(Func<Access, object> propertyAccessor)
        {
            _propertyAccessor = propertyAccessor;
        }

        public DatabaseEngine Resolve(Access source, TDestination destination, DatabaseEngine destMember, ResolutionContext context)
        {
            var value = _propertyAccessor(source).ToString();

            return string.IsNullOrWhiteSpace(value)
            ? DatabaseEngine.Empty
            : JsonConvert.DeserializeObject<DatabaseEngine>(value);
        }
    }

    public class JsonResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, string>
    {
        private readonly Func<TSource, object> _propertyAccessor;

        public JsonResolver(Func<TSource, object> propertyAccessor)
        {
            _propertyAccessor = propertyAccessor;
        }

        public string Resolve(TSource source, TDestination destination, string destMember, ResolutionContext context)
        {
            var value = _propertyAccessor(source);
            return JsonConvert.SerializeObject(value);
        }
    }


}
