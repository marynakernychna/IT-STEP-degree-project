using AutoMapper;
using Core.DTO.User;

namespace Core.Helpers.ApplicationProfiles
{
    public class User : Profile
    {
        public User()
        {
            CreateMap<Entities.User, UserProfileInfoDTO>();
        }
    }
}
