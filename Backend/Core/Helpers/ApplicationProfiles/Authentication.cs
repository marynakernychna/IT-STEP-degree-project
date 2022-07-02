using AutoMapper;
using Core.DTO.Authentication;
using Core.Entities;

namespace Core.Helpers.ApplicationProfiles
{
    public class Authentication : Profile
    {
        public Authentication()
        {
            CreateMap<UserRegistrationDTO, User>()
                .ForMember(dest => dest.UserName,
                    act => act.MapFrom(src => src.Email));
        }
    }
}
