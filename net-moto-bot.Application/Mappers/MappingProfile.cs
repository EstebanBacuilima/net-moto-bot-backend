using AutoMapper;
using net_moto_bot.Application.Dtos.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserChatResponseDto, UserChat>().ReverseMap();
    }
}
