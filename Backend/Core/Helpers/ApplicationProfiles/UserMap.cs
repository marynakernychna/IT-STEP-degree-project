using AutoMapper;
using Core.DTO.User;
using Core.Entities;

namespace Core.Helpers.ApplicationProfiles
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserProfileInfoDTO>();
        }
    }
}
