using AutoMapper;
using CDN.Core.Entities;
using CDN.Dtos;

namespace CDN.Helpers;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>();
    }
}
