using AutoMapper;
using Core.DTO.Characteristic;
using Core.Entities;

namespace Core.Helpers.ApplicationProfiles
{
    public class CharacteristicMap : Profile
    {
        public CharacteristicMap()
        {
            CreateMap<Characteristic, CharacteristicWithoutWareIdDTO>().ReverseMap();
        }
    }
}
