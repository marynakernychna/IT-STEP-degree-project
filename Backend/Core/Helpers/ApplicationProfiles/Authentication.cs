using AutoMapper;
using Core.DTO.Authentication;

namespace Core.Helpers.ApplicationProfiles
{
    public class Authentication : Profile
    {
        public Authentication()
        {
            CreateMap<UserRegistrationDTO, Entities.User>()
                .ForMember(dest => dest.UserName,
                    act => act.MapFrom(src => src.Email));
        }
    }
}
