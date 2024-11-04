using AutoMapper;
using CDN.Core.Entities;
using CDN.Dtos;

namespace CDN.Helpers;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserAuditDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.ModifiedOn != null ? src.ModifiedOn : src.CreatedOn));
    }
}
