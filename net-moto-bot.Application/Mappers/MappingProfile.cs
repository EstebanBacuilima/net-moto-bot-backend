using AutoMapper;
using net_moto_bot.Application.Dtos.Custom;
using net_moto_bot.Application.Dtos.Public.Response;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserChatResponseDto, UserChat>().ReverseMap();

        CreateMap<BrandDTO, Brand>();
        CreateMap<Brand, BrandDTO>();

        CreateMap<CategoryDTO, Category>();
        CreateMap<Category, CategoryDTO>();

    }
}
