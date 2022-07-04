using AutoMapper;
using Core.DTO.UserDTO;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers.ApplicationProfiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserProfileInfoDTO>().ReverseMap();
        }
    }
}
